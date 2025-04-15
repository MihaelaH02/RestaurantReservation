using RestaurantReservation.DTO.Email;
using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository.System;
using RestaurantReservation.Repository.Users;
using RestaurantReservation.Services.EmailSender;
using System.Text.Json;

namespace RestaurantReservation.Services.Users
{
    /// <summary> Сервиз за обработка на акаунти </summary>
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IMailService _emailSenderService;
        private readonly IJwtService _jwtService;

        public AccountsService(IAccountsRepository repository
                              , IJwtService jwtService
                              , IMailService emailSenderService)
        {
            _accountRepository = repository;
            _jwtService = jwtService;
            _emailSenderService = emailSenderService;
        }

        public async Task<string?> Register(JsonElement jsonRequest)
        {
            MailRequest mailRequest = new MailRequest();
            Accounts? response = await _accountRepository.RegisterAsync(jsonRequest, mailRequest);

            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (mailRequest == null)
                throw new ArgumentNullException(nameof(mailRequest));

            await _emailSenderService.SendEmailAsync(mailRequest);

            return _jwtService.GenerateToken(response.Id.ToString(), response.Role.Role);
        }

        public async Task<string?> Login(JsonElement jsonRequest)
        {
            MailRequest mailRequest = new MailRequest();
            LoginDTO requestDTO = JsonSerializer.Deserialize<LoginDTO>(jsonRequest.GetRawText());
            Accounts? response = await _accountRepository.LoginAsync(requestDTO, mailRequest);

            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (mailRequest != null)
                await _emailSenderService.SendEmailAsync(mailRequest);

            return _jwtService.GenerateToken(response.Id.ToString(), response.Role.Role);
        }
    }
}
