using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Models.Users
{
    [Table("CLIENTS")]
    public class Clients
    {
        public Clients() { }
        ~Clients() { }


        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("ACCOUNT_ID")]
        [ForeignKey("ACCOUNT_ID")]
        public Accounts Account { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("POINTS")]
        public int Points { get; set; }
    }
}
