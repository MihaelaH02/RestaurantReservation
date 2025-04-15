using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.DTO.RegisterDTO
{
    /// <summary> Клас за регистрация на ресторант </summary>
    public class RestaurantRegisterDTO
    {
        public RestaurantRegisterDTO() { }

        public RegisterDTO Account { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long Bulstat { get; set; }
        public RestaurantAtmosphere Atmosphere { get; set; }
    }
}