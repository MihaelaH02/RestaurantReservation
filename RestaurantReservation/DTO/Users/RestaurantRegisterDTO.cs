using RestaurantReservation.Models.Nomenclatures;

namespace RestaurantReservation.DTO.RegisterDTO
{
    /// <summary> Клас за регистрация на ресторант </summary>
    public class RestaurantRegisterDTO
    {
        public RestaurantRegisterDTO(RegisterDTO account
                                    , string companyName
                                    , string description
                                    , string address
                                    , long bulstat
                                    , RestaurantAtmosphere atmosphere)
        {
            Account     = account;
            CompanyName = companyName;
            Description = description;
            Address     = address;
            Bulstat     = bulstat;
            Atmosphere  = atmosphere;
        }


        public RegisterDTO Account { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public long Bulstat { get; set; }
        public RestaurantAtmosphere Atmosphere { get; set; }
    }
}