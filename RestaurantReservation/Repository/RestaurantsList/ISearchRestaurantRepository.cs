using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Models.AdditionalFunctionality;

namespace RestaurantReservation.Repository.RestaurantsList
{
    public interface ISearchRestaurantRepository
    {
        Task<List<RestaurantsListsResponseDTO>> GetFilteredRestaurantsAsync(Guid uuid);
        Task SaveSearchFiltersAsync(Guid uuid, List<RestaurantSearchFilter> filters);

        Task ClearSearchFiltersAsync(Guid uuid);

    }
}
