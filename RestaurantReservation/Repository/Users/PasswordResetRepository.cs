using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.Email;
using RestaurantReservation.DTO.Users;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository.System;
using static RestaurantReservation.Common.GlobalEnums.GlobalEnums;

namespace RestaurantReservation.Repository.Users
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Accounts> _passwordHasher;
        private readonly IEmailRepository _mailRepository;

        private const int MIN_SYMBOLS_TOKEN = 0;
        private const int MAX_SYMBOLS_TOKEN = 6;
        private const int EXPIRE_TOKEN_MIN  = 5;

        public PasswordResetRepository( ApplicationDbContext applicationDb
                                      , IPasswordHasher<Accounts> passwordHasher
                                      , IEmailRepository mailRepository)
        {
            _context = applicationDb;
            _passwordHasher = passwordHasher;
            _mailRepository = mailRepository;
        }

        public async Task<MailRequest> SendResetCodeAsync(String email)
        {
            var token = Guid.NewGuid().ToString().Substring(MIN_SYMBOLS_TOKEN, MAX_SYMBOLS_TOKEN).ToUpper();
            var expiresAt = DateTime.UtcNow.AddMinutes(EXPIRE_TOKEN_MIN);

            var oldTokens = _context.PasswordResetTokens.Where(t => t.Email == email);
            _context.PasswordResetTokens.RemoveRange(oldTokens);

            await _context.PasswordResetTokens.AddAsync(new PasswordResetToken
            {
                Email = email,
                Token = token,
                ExpiresAt = expiresAt
            });

            await _context.SaveChangesAsync();

            MailRequest mailRequest = await _mailRepository.LoadMailTemplate((short)NotificationEventsTypes.BlockedUser);
            if (mailRequest == null)
                throw new ArgumentNullException(nameof(mailRequest));

            mailRequest.ToEmail = email;
            mailRequest.Body = mailRequest.Body
                                .Replace("{CODE}", token)
                                .Replace("{APP_NAME}", SYSTEM_DEFINES.APP_NAME);

            return mailRequest;
        }

        public async Task<Accounts> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            Accounts? account = _context.Accounts
                                            .Include(a => a.Role)
                                            .FirstOrDefault(a => a.Email == resetPassword.Email);

            if (account == null)
                throw new ErrorMsg(ERROR_MSG.MSG_ACCOUNTS_PASWORD_RESET_WRONG_EMAIL);

            if (!await ValidateTokenAsync(resetPassword.Email, resetPassword.Token))
                throw new ErrorMsg(ERROR_MSG.MSG_ACCOUNTS_PASWORD_RESET_WRONG_TOKEN);

            _context.PasswordResetTokens.RemoveRange(_context.PasswordResetTokens
                                            .Where(t => t.Email == resetPassword.Email));

            account.Password = _passwordHasher.HashPassword(account, account.Password);
            account.LastChangeAt = DateTime.UtcNow;
            account.BlockedAt = SYSTEM_DEFINES.NON_DATE;
            account.CurrentAccessFailCount = 0;
            GlobalMethods.SetBit(account.Status, AccountStatusBits.STS_ACTIVE, true);
            GlobalMethods.SetBit(account.Status, AccountStatusBits.STS_BLOCKED, false);

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> ValidateTokenAsync(string email, string token)
        {
            var found = await _context.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.Email == email && t.Token == token);

            return found != null && found.ExpiresAt > DateTime.UtcNow;
        }
    }
}
