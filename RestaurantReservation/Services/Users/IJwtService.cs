using System.Text.Json;
using Microsoft.Extensions.Options;

namespace RestaurantReservation.Services.Users
{
    public interface IJwtService
    {
        Task SendResetCodeAsync(string email);
        Task<string?> ResetPasswordAsync(JsonElement paswordResetRequest);
        string GenerateToken(string userId, string role);

    }
}
