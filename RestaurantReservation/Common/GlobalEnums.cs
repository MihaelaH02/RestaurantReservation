namespace RestaurantReservation.Common.GlobalEnums
{
    public static class GlobalEnums
    { 
        public enum WeekDays : short
        {
            // [ComponentModel.Description("Example One")]
            Monday = 1,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        public enum UserRoles : short
        {
            Admin = 1,
            Restaurant,
            Client
        }

        public enum NotificationEventsTypes : short
        {
            Registration = 1,
            ChangePassword,
            BlockedUser,
            ReservationRequest,
            ApprovedReservationRequest,
            CancledReservationRequest,
            Feedback,
            PointsDeducted,
            Information,
            Other
        }
    }
}
