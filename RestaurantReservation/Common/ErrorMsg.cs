namespace RestaurantReservation.Common
{
    public class ErrorMsg : Exception
    {
        public string Message { get; set; }

        public ErrorMsg(string message)
        {
            Message = message;
        }
    }
}
