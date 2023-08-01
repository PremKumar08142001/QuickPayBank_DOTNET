using AuthenticationAPI.Models;
using AuthenticationAPI.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AuthenticationAPI.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthenticationAPI.Services
{
    public class AuthService: IAuthService
    {
        IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration; 
        }
        public async Task<List<Claim>> Login(LoginModel model)
        {
            List<Claim> Temp = await _authRepository.Login(model);
            if (Temp.Count > 0) {
                return Temp;
            }
            throw new UserNotFoundException($"User with id: {model.UserName} does not exist");


        }
        public async Task<bool> RegisterCustomer(Register model)
        {
             if( await _authRepository.RegisterCustomer(model)== true)
            {
                return true;
            }
           throw new UserAlreadyExistException($"User with id: {model.UserName} Already exist");

        }
        public async Task<bool> RegisterManager(Register model)
        {
            if (await _authRepository.RegisterManager(model))
            {
                return true;
            }
            throw new UserNotFoundException($"User with id: {model.UserName} Already exist");

        }

        public async Task<bool> RegisterAdmin(Register model)
        {
            if (await _authRepository.RegisterAdmin(model))
            {
                return true;
            }
            throw new UserNotFoundException($"User with id: {model.UserName} Already exist");
        }
        public JwtSecurityToken GetToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
                );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey,
                                        SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        public async Task<IdentityUser> GetByUserName(string name)
        {
            var user = await _authRepository.GetByUserName(name);
            if(user == null)
            {
                throw new UserNotFoundException($"User with {name} Already exist");
            }
            return user;
        }
        public async Task<List<string>> GetALlManagers()
        {
            var user = await _authRepository.GetALlManagers();
            if (user.Count()==0)
            {
                throw new UserNotFoundException($"No Manager Found");
            }
            return user;
        }
        
    }
}
