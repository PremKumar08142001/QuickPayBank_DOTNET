using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace TransactionMonitoringAPI.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }

        [JsonIgnore]
        public DateTime TransactionDate { get; set; }

        // [JsonIgnore]
        public string Status { get; set; } = TransactionStatus.Completed;

        public int UserId { get; set; }
        public string Name { get; set; }

        public string Ifsccode { get; set; }
        /* [JsonIgnore]
         public Account Account { get; set; }

         [JsonIgnore]

         [JsonIgnore]
         public User User { get; set; }*/
    }
}
