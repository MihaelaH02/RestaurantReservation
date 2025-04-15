using System.Text.Json;

namespace RestaurantReservation.Services.Users
{
    public interface IPasswordResetService
    {
        Task SendResetCodeAsync(string email);
        Task<string?> ResetPasswordAsync(JsonElement paswordResetRequest);
    }
}
