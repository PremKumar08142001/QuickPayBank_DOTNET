using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Models
{
    public class LoanOffered
    {

        [Key]
        public int LoanOfferedID { get; set; }

        public string LoanType { get; set; }
        public string image { get; set; }

        public double Interest { get; set; }
    }
        
}
