using Azure.Identity;
using RestaurantReservation.Models.Nomenclatures;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Models.Users
{
    public static class AccountStatusBits
    {
        public const int STS_ACTIVE             = 1; // 1 - активен профил
        public const int STS_BLOCKED            = 2; // 2 - блокиран профил
        public const int STS_EMAIL_CONFIRMED    = 3; // 3 - потвърден имейл
        public const int STS_PHONE_CONFIRMED    = 4; // 4 - потвърден телефон
    }

    [Table("ACCOUNTS")]
    public class Accounts
    {
        public Accounts() { }
        ~Accounts() { }

        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public short Id { get; set; }

        [Column("STATUS")]
        public byte[] Status { get; set; }

        [Column("ROLE_ID")]
        [ForeignKey("ROLE_ID")]
        public AccountRoleType Role { get; set; }

        [Column("USERNAME")]
        public string Username {  get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PHONE")]
        public string Phone { get; set; }

        [Column("ACCESS_FAIL_COUNT")]
        public short AccessFailCount { get; set; }

        [Column("CURRENT_ACCESS_FAIL_COUNT")]
        public short CurrentAccessFailCount { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }
        
        [Column("LAST_CHANGE_AT")]
        public DateTime LastChangeAt { get; set; }

        [Column("BLOCKED_AT")]
        public DateTime BlockedAt { get; set; }
    }
}
