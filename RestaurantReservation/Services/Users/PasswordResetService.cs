using RestaurantReservation.DTO.Email;
using RestaurantReservation.DTO.Users;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository.Users;
using RestaurantReservation.Services.EmailSender;
using System;
using System.Text.Json;

namespace RestaurantReservation.Services.Users
{    
    /// <summary> Сервиз за обработка на заявки за смяна на парола </summary>
    public class PasswordResetService: IPasswordResetService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordResetRepository _passwordResetRepository;
        private readonly IMailService _emailSenderService;

        public PasswordResetService(IJwtService jwtService
                                   , IPasswordResetRepository passwordResetRepository
                                   , IMailService mailSenderService)
        {
            _jwtService = jwtService;
            _emailSenderService = mailSenderService;
            _passwordResetRepository = passwordResetRepository;
        }

        public async Task SendResetCodeAsync(string email)
        {
            MailRequest? mailRequest = await _passwordResetRepository.SendResetCodeAsync(email);
            if (mailRequest == null)
                throw new ArgumentNullException(nameof(mailRequest));

            await _emailSenderService.SendEmailAsync(mailRequest);
        }

        public async Task<string?> ResetPasswordAsync(JsonElement paswordResetRequest)
        {
            ResetPasswordDto? resetPasswordDto = JsonSerializer.Deserialize<ResetPasswordDto>(paswordResetRequest.GetRawText());
            if (resetPasswordDto == null)
                throw new ArgumentNullException(nameof(resetPasswordDto));

            Accounts? account = await _passwordResetRepository.ResetPasswordAsync(resetPasswordDto);
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return _jwtService.GenerateToken(account.Id.ToString(), account.Role.Role);
        }
    }
}
