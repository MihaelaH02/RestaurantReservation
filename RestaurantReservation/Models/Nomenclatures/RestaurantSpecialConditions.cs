using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RestaurantReservation.Models.Nomenclatures
{
    public static class SpecialConditionsStatusBits
    {
        public const int STS_NOT_ACCEPT = 1; // 1 - не приема резервации онлайн
    }

    [Table("RESTAURANT_SPECIAL_CONDITIONS")]
    public class RestaurantSpecialConditions
    {
        public RestaurantSpecialConditions() { }
        ~RestaurantSpecialConditions() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("STATUS")]
        public byte[] Status { get; set; }

        [Column("SPECIAL_CONDITION")]
        public string SpecialCondition { get; set; }
    }
}
