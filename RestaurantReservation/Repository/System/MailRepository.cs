using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.Email;
using RestaurantReservation.Models.Reservation;
using RestaurantReservation.Models.System;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Services.EmailSender;
using System.Text.Json;

namespace RestaurantReservation.Repository.System
{
    public class MailRepository
    {

        private readonly ApplicationDbContext _context;

        public MailRepository(ApplicationDbContext context)
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
                throw new Exception("Email template not found.");

            string title = template.Title.Replace("", SYSTEM_DEFINES.APP_NAME);

            return new MailRequest
            {
                Subject = title,
                Body = template.Description
            };
        }
    }
}
