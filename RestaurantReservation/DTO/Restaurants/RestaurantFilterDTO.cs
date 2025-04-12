using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.DTO.Restaurants
{
    /// <summary> Клас за обработка на филтрите за търсене на ресторант </summary>
    public class RestaurantFilterDTO
    {
        public RestaurantFilterDTO( List<RestaurantAtmosphere> atmospheres
                                  , List<RestaurantLocations> locations
                                  , List<DateTime> dates
                                  , double rating) 
        {
            Atmospheres = atmospheres;
            Locations = locations;
            Dates = dates;
            Rating = rating;
        }

        public List<RestaurantAtmosphere>? Atmospheres { get; set; }
        public List<RestaurantLocations>? Locations { get; set; }
        public List<DateTime>? Dates { get; set; }
        public double? Rating { get; set; }
    }
}