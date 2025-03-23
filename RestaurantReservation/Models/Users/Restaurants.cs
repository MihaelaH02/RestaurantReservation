using RestaurantReservation.Models.Nomenclatures;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Users
{
    [Table("RESTAURANTS")]
    public class Restaurants
    {
        public Restaurants() { }
        ~Restaurants() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("ACCOUNT_ID")]
        [ForeignKey("ACCOUNT_ID")]
        public Accounts Account { get; set; }

        [Column("COMPANY_NAME")]
        public string CompanyName { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("BULSTAT")]
        public long Bulstat { get; set; }

        [Column("ATMOSPHERE_ID")]
        [ForeignKey("ATMOSPHERE_ID")]
        public RestaurantAtmosphere Atmosphere { get; set; }

        [Column("RATING")]
        public float Rating { get; set; }

        [Column("KEEP_RES_TABLE_TIME")]
        public float KeepResTableTime { get; set; }

        [Column("DEFAULT_MAX_RE_DURATION")]
        public float DefaultMaxResDuration { get; set; }
    }
}