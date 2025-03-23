using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Nomenclatures
{
    [Table("NOTIFICATION_EVENTS")]
    public class NotificationEvents
    {
        public NotificationEvents() { }
        ~NotificationEvents() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("EVENT")]
        public string Event { get; set; }
    }
}
