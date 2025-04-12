using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Services;

namespace RestaurantReservation.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /*[HttpGet("filter")]
        public async Task<IActionResult> GetFilteredRestaurants([FromQuery] RestaurantFilterDTO filter)
        {
            var restaurants = await _restaurantService.GetFilteredRestaurants(filter);
            return Ok(restaurants);
        }*/
    }
}
