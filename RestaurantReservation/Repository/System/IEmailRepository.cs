using RestaurantReservation.DTO.Email;

namespace RestaurantReservation.Repository.System
{
    public interface IEmailRepository
    {
       Task<MailRequest> LoadMailTemplate(short eventId, short? restaurantId = null);
    }
}
