
namespace RestaurantReservation.DTO.RegisterDTO
{
    /// <summary> Клас за регистрация на акаунт </summary>
    public class RegisterDTO
    {
        public RegisterDTO() { }

        public short Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
