﻿namespace RestaurantReservation.DTO.Email
{
    public class MailRequest
    {
        public MailRequest() { }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
