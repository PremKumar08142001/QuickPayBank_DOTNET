using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
//using AuthenticationAPI.Migrations;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public AuthRepository
            (
                UserManager<User> userManager,
                RoleManager<IdentityRole> roleManager
               
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
           
        }
        public async Task<List<Claim>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var authClaims = new List<Claim>();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                authClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                

            }
            return authClaims;
        }
        public async Task<bool> RegisterCustomer(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            
            if (userExists == null)
            {
                Console.WriteLine($"{userExists}");

                IdentityUser user = new User()
                {
                   
                    PhoneNumber = model.UserPhone,
                    UserGender = model.UserGender,
                    Email = model.UserEmail,
                    SecurityStamp =  model.UserAddress,
                    UserName = model.UserName,
                    UserAddress= model.UserAddress,
                    UserId= _userManager.Users.Count()+1000
                };
                var result = await _userManager.CreateAsync((User)user, model.Password);
               

                if (!result.Succeeded)
                    return false;
                if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
                {
                    await _userManager.AddToRoleAsync((User)user, UserRoles.Customer);
                }
                return true;
            }
            return false;
        }
        public async Task<bool> RegisterManager(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists == null)
            {

                IdentityUser user = new User()
                {

                    PhoneNumber = model.UserPhone,
                    UserGender = model.UserGender,
                    Email = model.UserEmail,
                    SecurityStamp = model.UserAddress,
                    UserName ="M-"+model.UserName,
                    UserAddress = model.UserAddress,
                };
                var result = await _userManager.CreateAsync((User)user, model.Password);
                if (!result.Succeeded)
                    return false;
                
                if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
                {
                    await _userManager.AddToRoleAsync((User)user, UserRoles.Customer);
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
                {

                    await _userManager.AddToRoleAsync((User)user, UserRoles.Manager);
                }
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterAdmin(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists == null)
            {

                IdentityUser user = new User()
                {

                    PhoneNumber = model.UserPhone,
                    UserGender = model.UserGender,
                    Email = model.UserEmail,
                    SecurityStamp = model.UserAddress,
                    UserName = model.UserName,
                    UserAddress = model.UserAddress,
                };
                var result = await _userManager.CreateAsync((User)user, model.Password);
                if (!result.Succeeded)
                    return false;
       
                if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Customer))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
                {
                    await _userManager.AddToRoleAsync((User)user, UserRoles.Manager);
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Customer))
                {
                    await _userManager.AddToRoleAsync((User)user, UserRoles.Customer);
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync((User)user, UserRoles.Admin);
                }

                return true;
            }
            return false;
        }
        public async Task<IdentityUser> GetByUserName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

          
            return user;

        }
        public async Task<List<string>> GetALlManagers()
        {

            var users = await _userManager.Users
     .Where(user => user.UserName.StartsWith("M-")).Select(user => user.UserName)
     .ToListAsync();
            return users;
        }
    }
}

