using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace AcountAPI.Models
{
    public class Account
    {
        [Key]
        public long AccountNumber { get; set; }
        public string BranchCode{ get; set; }
        public string BranchName { get; set; }
        public long Balance { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public Account()
        {
            Balance = 10000;
        }
    }
}
