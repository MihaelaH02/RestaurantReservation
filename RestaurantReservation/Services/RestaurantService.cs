using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Models.Users;
using RestaurantReservation.Repository;

namespace RestaurantReservation.Services
{
    /// <summary> Зареждане на всички ресторанти по подадени филтри </summary>
    public class RestaurantService
    {
        private readonly RestaurantFilrerRepository _restaurantFilterRepository;

        public RestaurantService(RestaurantFilrerRepository restaurantRepository)
        {
            _restaurantFilterRepository = restaurantRepository;
        }

/*        public async Task<IEnumerable<Restaurants>> GetFilteredRestaurants(RestaurantFilterDTO filter)
        {
            return await _restaurantFilterRepository.LoadFilteredRestaurantsAsync(filter);
        }*/
    }
}
