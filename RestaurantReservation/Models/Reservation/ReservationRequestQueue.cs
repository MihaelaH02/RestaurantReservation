using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Reservation
{
    [Table("RESERVATION_REQUEST_QUEUE")]
    public class ReservationRequestQueue
    {
        public ReservationRequestQueue() { }
        ~ReservationRequestQueue() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESERVATION_ID")]
        [ForeignKey("RESERVATION_ID")]
        public Reservations Reservation { get; set; }
    }
}
