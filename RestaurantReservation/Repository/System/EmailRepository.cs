using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.Email;
using RestaurantReservation.Models.System;

namespace RestaurantReservation.Repository.System
{
    public class EmailRepository : IEmailRepository
    {

        private readonly ApplicationDbContext _context;

        public EmailRepository( ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary> Извличане на темплейт за имейл по подадено събитие и ресторант </summary>
        public async Task<MailRequest> LoadMailTemplate(short eventId, short? restaurantId = null)
        {
            NotificationSettings? template = await _context.NotificationSettings
                                                .Where( t => t.Event.Id == eventId
                                                     && t.Restaurant.Id == restaurantId)
                                                .FirstOrDefaultAsync();
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            return new MailRequest
            {
                Subject = template.Title.Replace("{APP_NAME}", SYSTEM_DEFINES.APP_NAME),
                Body = template.Description
            };
        }
    }
}
