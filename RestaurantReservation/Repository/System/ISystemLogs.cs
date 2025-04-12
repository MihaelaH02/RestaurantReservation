namespace RestaurantReservation.Repository.System
{
    public interface ISystemLogs
    {
        Task LogErrorAsync(Exception ex, int? accountId = null);
        Task LogMessageAsync(string message, string status, int? accountId = null);
    }
}
