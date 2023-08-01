using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Models
{
    public class LoanApplied
    {
        [Key]
        public int LoanAppliedID { get; set; }
        public string LoanType { get; set; }
        public string UserId { get; set; }
        public string UserName  { get; set; }
        public double LoanAmount { get; set; }
        public double AmountToPay { get; set; }
        public int Tenure { get; set; }
        public double Interest { get; set; }
        public string LoanStatus { get; set; }
        public string Comment { get; set; }
    }
}
