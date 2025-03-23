using RestaurantReservation.Models.RestaurantsSettings;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Reservation
{
    [Table("RESTAURANT_MONTHLY_SCHEDULE")]
    public class RestaurantMonthlySchedule
    {
        public RestaurantMonthlySchedule() { }
        ~RestaurantMonthlySchedule() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("DAY")]
        public DateTime Day { get; set; }

        [Column("SHEME_ID")]
        [ForeignKey("SHEME_ID")]
        public RestaurantCapacitySheme Scheme { get; set; }

        [Column("FREE_TABLES")]
        public int FreeTables { get; set; }
    }
}
