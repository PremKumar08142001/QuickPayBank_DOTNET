using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthenticationAPI.Models;
using AuthenticationAPI.Services;
using System.Diagnostics.Metrics;

namespace AuthenticationAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            List<Claim> temp = await _authService.Login(model);
            var token = _authService.GetToken(temp);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            }); ;


        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {

            try
            {
                await _authService.RegisterCustomer(model);

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            try
            {
                await _authService.RegisterAdmin(model);

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }
        }

        [HttpPost]
        [Route("register-manager")]
        public async Task<IActionResult> RegisterManager([FromBody] Register model)
        {
            try
            {
                await _authService.RegisterManager(model);

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }


        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByUserName(string name)
        {
            try
            {


                return Ok(await _authService.GetByUserName(name));

            }
            catch
            {
                return NotFound("User not found");
            }



        }
        [HttpGet]
        public async Task<IActionResult> GetALlManagers()
        {
            try
            {


                return Ok(await _authService.GetALlManagers());

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
    