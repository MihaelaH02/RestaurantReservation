using RestaurantReservation.Common;
using RestaurantReservation.Models.System;

namespace RestaurantReservation.Repository.System
{
    public class SystemLogsRepository : ISystemLogs
    {
        private readonly ApplicationDbContext _context;

        public SystemLogsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogErrorAsync(Exception ex, int? accountId = null)
        {
            var log = new SystemLogs
            {
                MsgStatus = SYSTEM_DEFINES.STATUS_ERROR_LOG,
                Date = DateTime.UtcNow,
                Log = ex.ToString(),
                Account = accountId.HasValue ? await _context.Accounts.FindAsync(accountId) : null
            };

            _context.SystemLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task LogMessageAsync(string message, string status, int? accountId = null)
        {
            var log = new SystemLogs
            {
                MsgStatus = status,
                Date = DateTime.UtcNow,
                Log = message,
                Account = accountId.HasValue ? await _context.Accounts.FindAsync(accountId) : null
            };

            _context.SystemLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
