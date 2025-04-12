using Microsoft.Identity.Client;

namespace RestaurantReservation.DTO.RegisterDTO
{
    /// <summary> Клас за регистрация на клиент </summary>
    public class ClientsRegisterDTO
    {
        public ClientsRegisterDTO() { }

        public RegisterDTO Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
