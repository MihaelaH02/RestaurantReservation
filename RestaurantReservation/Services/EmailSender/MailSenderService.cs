using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.Email;
using System.Net;
using System.Net.Mail;

namespace RestaurantReservation.Services.EmailSender
{
    public class MailSenderService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var smtpClient = new SmtpClient(_configuration["Email:SmtpHost"])
            {
                Port = int.Parse(_configuration["Email:SmtpPort"]),
                Credentials = new NetworkCredential(
               _configuration["Email:Username"],
               _configuration["Email:Password"]),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress( _configuration["Email:From"] ),
                Subject = mailRequest.Subject,
                Body = mailRequest.Body,
                IsBodyHtml = true
            };
            message.To.Add(mailRequest.ToEmail);

            await smtpClient.SendMailAsync(message);
        }
    }
}
