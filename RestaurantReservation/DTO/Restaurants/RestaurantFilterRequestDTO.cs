using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.DTO.Restaurants
{
    /// <summary> Клас за обработка на филтрите за търсене на ресторант </summary>
    public class RestaurantFilterRequestDTO
    {
        public RestaurantFilterRequestDTO(){ }

        public List<short>? Atmospheres { get; set; }
        public List<string>? Locations { get; set; }
        public List<DateTime>? Dates { get; set; }
        public double? Rating { get; set; }
    }
}