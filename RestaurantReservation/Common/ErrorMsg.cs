namespace RestaurantReservation.Common
{
    public class ErrorMsg : Exception
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public ErrorMsg(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
