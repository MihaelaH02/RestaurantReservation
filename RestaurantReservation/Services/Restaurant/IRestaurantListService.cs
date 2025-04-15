using RestaurantReservation.DTO.Restaurants;
using System.Text.Json;

namespace RestaurantReservation.Services.RestaurantsService
{
    public interface IRestaurantListService
    {
        Task<List<RestaurantsListsResponseDTO>> GetFilteredRestaurantsAsync(JsonElement jsonRequest);
    }
}
