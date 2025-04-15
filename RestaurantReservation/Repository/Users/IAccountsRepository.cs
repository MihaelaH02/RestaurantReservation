using RestaurantReservation.DTO.Email;
using System.Text.Json;
using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Users;

namespace RestaurantReservation.Repository.Users
{
    /// <summary> Интерфейс за оработка на акаунти </summary>
    public interface IAccountsRepository
    {
        Task<Accounts> RegisterAsync(JsonElement registerRequest, MailRequest mailRequest);
        Task<Accounts?> LoginAsync(LoginDTO dto, MailRequest mailRequest);
    }
}
