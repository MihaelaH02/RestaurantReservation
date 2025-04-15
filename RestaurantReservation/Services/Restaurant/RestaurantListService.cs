using System.Text.Json;
using RestaurantReservation.Common;
using RestaurantReservation.DTO.Restaurants;
using RestaurantReservation.Models.AdditionalFunctionality;
using RestaurantReservation.Repository.RestaurantsList;
using RestaurantReservation.Repository.RestaurantsList.RestaurantsList;

namespace RestaurantReservation.Services.RestaurantsService
{
    /// <summary> Зареждане на всички ресторанти по подадени филтри </summary>
    public class RestaurantListService : IRestaurantListService
    {
        private readonly ISearchRestaurantRepository _restaurantRepository;

        public RestaurantListService(ISearchRestaurantRepository repository)
        {
            _restaurantRepository = repository;
        }

        public async Task<List<RestaurantsListsResponseDTO>> GetFilteredRestaurantsAsync(JsonElement jsonRequest)
        {
            RestaurantFilterRequestDTO? requestDTO = JsonSerializer.Deserialize<RestaurantFilterRequestDTO>(jsonRequest.GetRawText());
            if (requestDTO == null)
                throw new ArgumentNullException(nameof(requestDTO));

            Guid uuid = Guid.NewGuid();

            await SaveFiltersInTable(uuid, requestDTO);

            List<RestaurantsListsResponseDTO>? response = await _restaurantRepository.GetFilteredRestaurantsAsync(uuid);
            if(response == null)
                throw new ErrorMsg(ERROR_MSG.MSG_RESTAURNATS_SEARCH_BY_FILERS_NONE);

            await _restaurantRepository.ClearSearchFiltersAsync(uuid);
            return response;
        }

        private async Task SaveFiltersInTable(Guid uuid, RestaurantFilterRequestDTO requestDTO)
        {
            List<RestaurantSearchFilter> filters = MapFilters(uuid, requestDTO);

            await _restaurantRepository.SaveSearchFiltersAsync(uuid, filters);
        }

        private List<RestaurantSearchFilter> MapFilters(Guid uuid, RestaurantFilterRequestDTO requestDTO)
        {
            List<RestaurantSearchFilter> filters = new List<RestaurantSearchFilter>();

            foreach (var atmosphere in requestDTO.Atmospheres)
            { 
                filters.Add(new RestaurantSearchFilter(uuid, FilterType.Atmosphere, atmosphere.ToString()));
            }

            foreach (var locations in requestDTO.Locations)
            {
                filters.Add(new RestaurantSearchFilter(uuid, FilterType.Location, locations.ToString()));
            }

            foreach (var dates in requestDTO.Dates)
            {
                filters.Add(new RestaurantSearchFilter(uuid, FilterType.Atmosphere, dates.ToString()));
            }

            filters.Add(new RestaurantSearchFilter(uuid, FilterType.Rate, requestDTO.Rating.ToString()));

            return filters;
        }
    }
}
