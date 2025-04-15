using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestaurantReservation.Models.Users
{
    [Keyless]
    [Table("ARG_PASSWORD_RESER_TOKENS")]
    public class PasswordResetToken
    {
        [Column("UUID")]
        public Guid UUID { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("TOKEN")]
        public string Token { get; set; }

        [Column("EXPIRE_AT")]
        public DateTime ExpiresAt { get; set; }
    }

}
