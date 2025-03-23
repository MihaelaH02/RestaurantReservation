using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Nomenclatures
{
    [Table("ACCOUNT_ROLE_TYPE")]
    public class AccountRoleType
    {
        public AccountRoleType() { }
        ~AccountRoleType() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("ROLE")]
        public string Role { get; set; }
    }
}
