using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.RestaurantsSettings
{
    [Table("RESTAURANT_CAPACITY_SCHEME")]
    public class RestaurantCapacitySheme
    {
        public RestaurantCapacitySheme() { }
        ~RestaurantCapacitySheme() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESTAURANT_ID")]
        [ForeignKey("RESTAURANT_ID")]
        public Restaurants Restaurant { get; set; }

        [Column("LOCATION_ID")]
        [ForeignKey("LOCATION_ID")]
        public RestaurantLocations Location { get; set; }

        [Column("TABLE_TYPE_ID")]
        [ForeignKey("TABLE_TYPE_ID")]
        public RestaurantTableTypes TableType { get; set; }

        [Column("TABLE_COUNT")]
        public int TableCount { get; set; }
    }
}
