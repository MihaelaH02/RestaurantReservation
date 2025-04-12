using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.RegisterDTO;

namespace RestaurantReservation.Repository.Users
{
    /// <summary> Клас за валидация на нов ресторант </summary>
    public class ValidateNewRestaurant : ValidationRepository<RestaurantRegisterDTO>
    {
        private readonly ApplicationDbContext _context;

        public ValidateNewRestaurant(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ErrorMsg?> Validate(RestaurantRegisterDTO newRestaurant)
        {
            if (newRestaurant == null)
                throw new ArgumentNullException(nameof(newRestaurant));

            if (await ValidateBulstat(newRestaurant.Bulstat).ConfigureAwait(false))
                return new ErrorMsg(false, string.Format(ERROR_MSG.MSG_ACCOUNTS_REGISTER_RESTAURANT_BULSTAT_TAKEDN, newRestaurant.Bulstat));

            return null;
        }

        /// <summary> Валидация на булстат </summary>
        private async Task<bool> ValidateBulstat(long bulstat)
        {
            return !await _context.Restaurants.AnyAsync(r => r.Bulstat == bulstat).ConfigureAwait(false);
        }
    }
}
