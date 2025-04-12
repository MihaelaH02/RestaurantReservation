using Microsoft.EntityFrameworkCore;
using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Models.Reservation;
using RestaurantReservation.Models.Users;

namespace RestaurantReservation.Repository
{
    public class RestaurantFilrerRepository
    {
        protected readonly DbSet<Restaurants> _dbSetRestaurants;
        protected readonly DbSet<RestaurantMonthlySchedule> _dbSetSchedule;
        // protected readonly DbSet<Restaurants> _dbSetRestaurants;


        /// <summary>Метод за зареждане на ресторанти по филтри </summary>
        /* public async Task<IEnumerable<Restaurants>> LoadFilteredRestaurantsAsync(RestaurantFilterDTO filter)
         {
             IQueryable<Restaurants> queryRestaurants = _dbSetRestaurants;
             IQueryable<RestaurantMonthlySchedule> queryLocationType = _dbSetSchedule
                 .Include(r => r.);


             if (filter.Atmosphere != null && filter.Atmosphere.Any())
                 queryRestaurants = queryRestaurants.Where(r => filter.Atmosphere.Contains(r.Atmosphere));

             if (filter.Location != null && filter.Location.Any())
                 query = query.Where(r => filter.Location.Contains(r));

             if (filter.Rating.HasValue)
                 queryRestaurants = queryRestaurants.Where(r => r.Rating >= filter.Rating.Value);

             if (filter.Rating.HasValue)
                   query = query.Where(r => r.Tables.Any(t => t.IsAvailable));

                 return await queryRestaurants.ToListAsync();
         }

     }*/
    }
}
