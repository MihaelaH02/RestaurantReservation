using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.RegisterDTO;

namespace RestaurantReservation.Repository.Users
{
    /// <summary> Клас за валидация на нов акаунт </summary>
    public class ValidateNewUser : ValidationRepository<RegisterDTO>
    {
        private readonly ApplicationDbContext _context;

        public ValidateNewUser(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ErrorMsg?> Validate(RegisterDTO newUser)
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));

            if (!await ValidateUsername(newUser.Username).ConfigureAwait(false))
                return new ErrorMsg(false, string.Format(ERROR_MSG.MSG_ACCOUNTS_REGISTER_USERNAME_TAKEN, newUser.Username) );

            if (!await ValidateEmail(newUser.Email).ConfigureAwait(false))
                return new ErrorMsg(false, string.Format(ERROR_MSG.MSG_ACCOUNTS_REGISTER_EMAIL_TAKEN, newUser.Email) );

            if (!await ValidatePhone(newUser.PhoneNumber).ConfigureAwait(false))
                return new ErrorMsg(false, string.Format(ERROR_MSG.MSG_ACCOUNTS_REGISTER_PHONE_TAKEN, newUser.PhoneNumber) );

            return null;
        }

        /// <summary> Валидация на потребителско име </summary>
        private async Task<bool> ValidateUsername(string username)
        {
            return !await _context.Accounts.AnyAsync(a => a.Username == username).ConfigureAwait(false);
        }

        /// <summary> Валидация на имейл адрес </summary>
        private async Task<bool> ValidateEmail(string email)
        {
            return !await _context.Accounts.AnyAsync(a => a.Email == email).ConfigureAwait(false);
        }

        /// <summary> Валидация на телефонен номер </summary>
        private async Task<bool> ValidatePhone(string phone)
        {
            return !await _context.Accounts.AnyAsync(a => a.Phone == phone).ConfigureAwait(false);
        }
    }
}
