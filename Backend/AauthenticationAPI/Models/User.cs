using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Models
{
    public class User: IdentityUser
    {
        public string UserGender { get; set; }
        public string UserAddress { get; set; }
        public int UserId { get;set; }

    }
}
