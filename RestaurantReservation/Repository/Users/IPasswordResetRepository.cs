using RestaurantReservation.DTO.Email;
using RestaurantReservation.DTO.Users;
using RestaurantReservation.Models.Users;

namespace RestaurantReservation.Repository.Users
{
    public interface IPasswordResetRepository
    {
        Task<MailRequest> SendResetCodeAsync(String email);
        Task<bool> ValidateTokenAsync(string email, string token);
        Task<Accounts> ResetPasswordAsync(ResetPasswordDto resetPassword);

    }
}