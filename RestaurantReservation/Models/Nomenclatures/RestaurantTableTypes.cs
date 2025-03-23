using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Nomenclatures
{
    [Table("RESTAURANT_TABLE_TYPES")]
    public class RestaurantTableTypes
    {
        public RestaurantTableTypes() { }
        ~RestaurantTableTypes() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("TABLE_TYPE")]
        public string TableType { get; set; }

        [Column("SEATS")]
        public short Seats { get; set; }
    }
}
