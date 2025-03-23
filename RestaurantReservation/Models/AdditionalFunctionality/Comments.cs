using RestaurantReservation.Models.Reservation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Other
{
    [Table("COMMENTS")]
    public class Comments
    {
        public Comments() {}
        ~Comments() { }


        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESERVATION_ID")]
        [ForeignKey("RESERVATION_ID")]
        public Reservations Reservation { get; set; }

        [Column("RATE")]
        public float Rate { get; set; }

        [Column("COMMENT")]
        public string Comment { get; set; }
    }

}
