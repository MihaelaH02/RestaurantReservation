using RestaurantReservation.Models.Nomenclatures;
using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RestaurantReservation.Models.System
{
    [Table("NOTIFICATION_DEFAULT_SETTINGS")]
    public class NotificationSettings
    {
        public NotificationSettings() { }
        ~NotificationSettings() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("RESTAURANT_ID")]
        [ForeignKey("RESTAURANT_ID")]
        public Restaurants? Restaurant { get; set; }

        [Column("EVENT_ID")]
        [ForeignKey("EVENT_ID")]
        public NotificationEvents Event { get; set; }

        [Column("TITLE")]
        public string Title { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }
    }
}
