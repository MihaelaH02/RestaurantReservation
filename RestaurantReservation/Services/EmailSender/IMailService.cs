using RestaurantReservation.DTO.Email;

namespace RestaurantReservation.Services.EmailSender
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
