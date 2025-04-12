using RestaurantReservation.DTO.RegisterDTO;
using RestaurantReservation.Models.Users;

namespace RestaurantReservation.Repository.Users
{
    /// <summary> Интерфейс за оработка на акаунти </summary>
    public interface IAccountsRepository
    {
        Task<Accounts> RegisterAsync(object dto);
        Task<Accounts?> LoginAsync(LoginDTO dto);
    }
}
