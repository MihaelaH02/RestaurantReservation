using Microsoft.VisualBasic;
using RestaurantReservation.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.System
{
    [Table("SYSTEM_LOGS")]
    public class SystemLogs
    {
        public SystemLogs() { }
        ~SystemLogs() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Column("MSG_STATUS")]
        public string MsgStatus { get; set; }

        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("LOG")]
        public string Log { get; set; }

        [Column("ACCOUNT_ID")]
        [ForeignKey("ACCOUNT_ID")]
        public Accounts Account { get; set; }
    }
}
