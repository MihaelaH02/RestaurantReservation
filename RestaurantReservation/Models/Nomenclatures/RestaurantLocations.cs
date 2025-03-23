using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Nomenclatures
{
    [Table("RESTAURANT_LOCATIONS")]
    public class RestaurantLocations
    {
        public RestaurantLocations() { }
        ~RestaurantLocations() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("LOCATION")]
        public string Location { get; set; }
    }
}
