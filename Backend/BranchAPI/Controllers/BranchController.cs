using BranchAPI.Models;
using BranchAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BranchAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] Branch branch)
        {
            try
            {
                await _branchService.CreateBranch(branch);

                return Ok("Branch Add successfuly");

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // Handle the specific inner exception
                    var innerException = ex.InnerException;
                    // Log or handle the inner exception accordingly
                    return NotFound(innerException);
                }
                else
                {
                    return NotFound(ex.Message);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                await _branchService.DeleteBranch(id);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByBranchBranchCode(String username)
        {
            try
            {
                var account = await _branchService.GetByBranchId(username);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBranch()
        {
            try
            {
                var accounts = await _branchService.GetAllBranch();

                return Ok(accounts);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /*[HttpGet]
        [Route("byUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            try
            {
                var accounts = await _branchService.GetAllByUserId(userId);

                return Ok(accounts);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }*/
        [HttpPut]
        public async Task<IActionResult> UpdateBranch(int branchId, Branch branch)
        {
            try
            {
                await _branchService.UpdateBranch( branchId,  branch);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        

        [HttpGet]
        [Route("branchelist")]
        public async Task<IActionResult> GetAllBrancheNames()
        {
            try
            {
                var branches = await _branchService.GetAllBranches();

                return Ok(branches);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
