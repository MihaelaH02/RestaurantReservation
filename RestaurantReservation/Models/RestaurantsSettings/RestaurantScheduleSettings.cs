using RestaurantReservation.Common.GlobalEnums;
using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Models.RestaurantsSettings
{
    [Table("RESTAURANT_SCHEDULE_SETTINGS")]
    public class RestaurantScheduleSettings
    {
        public RestaurantScheduleSettings() { }
        ~RestaurantScheduleSettings() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESTAURANT_ID")]
        [ForeignKey("RESTAURANT_ID")]
        public Restaurants? Restaurant { get; set; }

        [Column("SPECIFIC_DATE")]
        public DateTime SpecificDate { get; set; }

        [Column("WEEK_DAY")]
        public GlobalEnums.WeekDays WeekDay { get; set; }

        [Column("HOUR_FROM")]
        public short HourFrom { get; set; }

        [Column("HOUR_TO")]
        public short HourTo { get; set; }

        [Column("SPECIAL_CONDITION_ID")]
        [ForeignKey("SPECIAL_CONDITION_ID")]
        public RestaurantSpecialConditions RestaurantSpecialCondition { get; set; }
    }
}
