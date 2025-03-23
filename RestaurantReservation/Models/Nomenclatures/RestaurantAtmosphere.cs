using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Nomenclatures
{
    [Table("RESTAURANT_ATMOSPHERE")]
    public class RestaurantAtmosphere
    {
        public RestaurantAtmosphere() { }
        ~RestaurantAtmosphere() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("ATMOSPHERE")]
        public string Atmosphere { get; set; }
    }
}
