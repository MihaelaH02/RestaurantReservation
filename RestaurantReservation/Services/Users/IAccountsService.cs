using System.Text.Json;

namespace RestaurantReservation.Services.Users
{
    public interface IAccountsService
    {
        Task<string?> Register(JsonElement jsonRequest);
        Task<string?> Login(JsonElement jsonRequest);
    }
}
