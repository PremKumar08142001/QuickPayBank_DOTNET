using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BranchAPI.Models
{
    public class Branch
    {
        [Key]
       
        public int BranchId { get; set; }
        public String BranchName { get; set; }
     //   [StringLength(10)]
        public string BranchCode { get; set; }
      //  public String BranchAddress { get; set; }
       public string UserName { get; set; }
        public Branch()
        { // Set the initial IFSC code value
            BranchCode = GenerateIFSCCode();
           //BranchId = _currentId;
        }

       // private static Int64 _currentId = 100; // Starting ID value
        private static int _currentCode = 1; // Starting code value

        private static string GenerateIFSCCode()
        {
            string code = $"QPAY0{_currentCode:D6}";
            _currentCode++;
            //_currentId++;
            return code;
        }
    }
}
