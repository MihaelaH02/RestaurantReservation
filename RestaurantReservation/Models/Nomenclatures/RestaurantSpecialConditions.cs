using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RestaurantReservation.Models.Nomenclatures
{
    public static class SpecialConditionsStatusBits
    {
        public const int STS_NOT_ACCEPT = 1; // 1 - неприема резервации онлайн
        public const int STS_ACCESPT    = 2; // 2 - приема резервации онлайн
       /* public const int STS_PRIVATE_EVENT  = 3; // 3 - часто събите
        public const int STS_NOT_ACCEPT_RES = 4; // 4 - не приема резервации
        public const int STS_LIVE_MUSIC     = 5; // 5 - жива музика*/
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
