using RestaurantReservation.Common;
using RestaurantReservation.DTO.Email;
using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository.System;
using RestaurantReservation.Repository.Users;
using RestaurantReservation.Services.EmailSender;
using System.Text.Json;
using RestaurantReservation.Common.GlobalEnums;

namespace RestaurantReservation.Services.Users
{
    /// <summary> Сервиз за обработка на акаунти </summary>
    public class AccountsService
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMailService _emailSenderService;
        private readonly JwtService _jwtService;
        private readonly MailRepository _mailRepository;

        public AccountsService( AccountRepository repository
                              , JwtService jwtService
                              , MailRepository mailRepository
                              , IMailService emailSenderService)
        {
            _accountRepository = repository;
            _jwtService = jwtService;
            _mailRepository = mailRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task<string?> Register(JsonElement registerRequest)
        {
            Accounts? account = await _accountRepository.RegisterAsync(registerRequest);

            if (account == null)
                throw new ArgumentNullException(nameof(account));

            MailRequest? mailRequest = await _mailRepository.LoadMailTemplate((short)NotificationTy.Registration);

            if (mailRequest == null)
                throw new ArgumentNullException(nameof(mailRequest));

            mailRequest.ToEmail = account.Email;
            mailRequest.Body = mailRequest.Body
                                .Replace("{NAME_USER}", account.Username)//името на потребителя!
                                .Replace("{LINK}", "test")
                                .Replace("{APP_NAME}", SYSTEM_DEFINES.APP_NAME);

            await _emailSenderService.SendEmailAsync(mailRequest);

            return _jwtService.GenerateToken(account.Id.ToString(), account.Role.Role);
        }

        public async Task<string?> Login(JsonElement loginRequest)
        { 
            LoginDTO loginDto = JsonSerializer.Deserialize<LoginDTO>(loginRequest.GetRawText());
            Accounts? account = await _accountRepository.LoginAsync(loginDto);

            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return _jwtService.GenerateToken(account.Id.ToString(), account.Role.Role);
        }

       /* public async Task<string?> ChangePassword(ChangePasswordDTO dto)
        {
            Accounts? account = await _accountRepository.ChangePassword(dto);
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return _jwtService.GenerateToken(account.Id.ToString(), account.Role.Role);
        }*/
    }
}
