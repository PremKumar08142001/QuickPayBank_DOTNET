using System.Security.Principal;
using AcountAPI.Data;
using AcountAPI.Models;
using AcountAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcountAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accService;
        public AccountController(IAccountService accService)
        {
            _accService = accService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccountReq([FromBody] Account account)
        {
            Console.WriteLine(".........start");
            try
            {
                await _accService.CreateAccountReq(account);

                return Ok(account);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountReq(int id)
        {
            try
            {
                await _accService.DeleteAccountReq(id);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByAccountReqId(long id)
        {
            try
            {
                var account=await _accService.GetByAccountReqId(id);

                return Ok(account);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("byBranchId/{branchId}")]
        public async Task<IActionResult> GetAllAccountReqsByBranchId(string branchId)
        {
            try
            {
                var accounts = await _accService.GetAllAccountReqsByBranchId(branchId);

                return Ok(accounts);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccountReques()
        {
            try
            {
                var accounts = await _accService.GetAllAccountReques();

                return Ok(accounts);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("updateDetails/{accountNumber}")]
        public async Task<IActionResult> UpdateAccountDetails(int accountNumber,String userName)
        {
            try
            {
                await _accService.UpdateAccountDetails(accountNumber,userName);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("updateBalance")]
        public async Task<IActionResult> UpdateAccountBalance([FromQuery] long accountnumber, [FromQuery] int balance)
        {
            try
            {
                Console.WriteLine(accountnumber + " +" + balance + "...........");
                await _accService.UpdateAccountBalance(accountnumber, balance);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
       
        [HttpGet]
        [Route("accountExist/{username}")]
        public async Task<IActionResult> AccountExist(string username)
        {
            try
            {
                
                return Ok(await _accService.AccountExist(username));

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("byName/{username}")]
        public async Task<IActionResult> ByName(string username)
        {
            try
            {

                return Ok(await _accService.ByName(username));

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("byId/{userId}")]
        public async Task<IActionResult> ById(int userId)
        {
            try
            {

                return Ok(await _accService.ById(userId));

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("byBranchcode/{branchCode}")]
        public async Task<IActionResult> ByBranchCode(string branchCode)
        {
            try
            {

                return Ok(await _accService.ByBranchCode(branchCode));

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}

