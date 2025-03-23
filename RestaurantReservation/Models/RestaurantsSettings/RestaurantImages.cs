using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.RestaurantsSettings
{
    [Table("RESTAURANT_IMAGES")]
    public class RestaurantImages
    {
        public RestaurantImages() { }
        ~RestaurantImages() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESTAURANT_ID")]
        [ForeignKey("RESTAURANT_ID")]
        public Restaurants Restaurant {  get; set; }

        [Column("IMAGE")]
        public string Image {  get; set; }
    }
}
