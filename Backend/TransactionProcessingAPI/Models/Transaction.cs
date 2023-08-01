using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text.Json.Serialization;
using TransactionAPI.Models;

namespace TransactionProcessingAPI.Models
{
    /*public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }

        // [JsonIgnore]
        // [DataType(DataType.DateTime)]
        //[Column(TypeName = "datetime2")]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        // [JsonIgnore]
        public string Status { get; set; } = TransactionStatus.Completed;
         
        // [JsonIgnore]
         public int UserId { get; set; }

        *//* [JsonIgnore]
         public Account Account { get; set; }


         [JsonIgnore]
         public User User { get; set; }*//*
    }*/


    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public decimal Amount { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Status { get; set; } = TransactionStatus.Completed;

        public int UserId { get; set; }
        public string Name { get; set; }

        public string Ifsccode { get; set; }    


     
    }




}
