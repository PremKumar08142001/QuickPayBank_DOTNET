using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthenticationAPI.Repository
{
    public interface IAuthRepository
    {
        Task<List<Claim>> Login(LoginModel model);
        Task<bool> RegisterCustomer(Register model);
        Task<bool> RegisterManager(Register model);
        Task<bool> RegisterAdmin(Register model);
        Task<IdentityUser> GetByUserName(string name);
        Task<List<string>> GetALlManagers();
    }
}
