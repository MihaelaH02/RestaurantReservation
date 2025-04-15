using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Reservation
{
    public static class ReservationStatusBits
    {
        public const int STS_SEND         = 1; // 1 - изпратена заявка
        public const int STS_AWAIT        = 2; // 2 - чака за потвърждение 
        public const int STS_ACCEPT       = 3; // 3 - приета заявка
        public const int STS_DENY         = 4; // 4 - отказана заявка
        public const int STS_VISITED      = 6; // 5 - посетил
        public const int STS_NOT_VISITED  = 7; // 5 - непосетил
        public const int STS_CLOSE        = 8; // 5 - закрита резервация
    }

    [Table("RESERVATIONS")]
    public class Reservations
    {
        public Reservations() { }
        ~Reservations() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("STATUS")]
        public byte[] Status { get; set; }

        [Column("RESTAURANT_ID")]
        [ForeignKey("RESTAURANT_ID")]
        public Restaurants Restaurants { get; set; }

        [Column("CLIENT")]
        [ForeignKey("CLIENT_ID")]
        public Clients Client { get; set; }

        [Column("REG_DATE")]
        public DateTime RegDate { get; set; }

        [Column("RESERVATION_DATE")]
        public DateTime ReservationDate { get; set; }

        [Column("STATUS_CHANGE_DATE")]
        public DateTime StatusChangeDate { get; set; }

        [Column("LOCATION_ID")]
        [ForeignKey("LOCATION_ID")]
        public RestaurantLocations Location { get; set; }

        [Column("TABLE_TYPE_ID")]
        [ForeignKey("TABLE_TYPE_ID")]
        public RestaurantTableTypes TableType { get; set; }

        [Column("PEOPLE_NUMBER")]
        public int PeopleNumber { get; set; }

        [Column("DURATION")]
        public float Duration { get; set; }

        [Column("POINTS_USED")]
        public short PointsUsed { get; set; }

        [Column("NAME_RESERVATION")]
        public string ResarvationName { get; set; }

        [Column("NOTE")]
        public string Note { get; set; }
    }
}
