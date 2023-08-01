using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAPI.Services
{
    public interface IAuthService
    {
        Task<List<Claim>> Login(LoginModel model);
        Task<bool> RegisterCustomer(Register model);
        Task<bool> RegisterManager(Register model);
        Task<bool> RegisterAdmin(Register model);
        JwtSecurityToken GetToken(List<Claim> claims);
        Task<IdentityUser> GetByUserName(string name);
        Task<List<string>> GetALlManagers();
    }
}
