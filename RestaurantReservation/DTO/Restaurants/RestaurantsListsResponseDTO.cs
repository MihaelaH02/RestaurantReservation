using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.DTO.Restaurants
{
    public class RestaurantsListsResponseDTO
    {
            public short Id { get; set; }
            public string CompanyName { get; set; }
            public string Address { get; set; }
            public float Rating { get; set; }
            public RestaurantAtmosphere Atmosphere { get; set; }
    }
}
