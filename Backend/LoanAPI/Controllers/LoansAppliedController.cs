using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanAPI.Data;
using LoanAPI.Models;
using LoanAPI.Service;

namespace LoanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansAppliedController : ControllerBase
    {
        private readonly ILoanAppliedService _service;

        public LoansAppliedController(ILoanAppliedService service)
        {
            _service = service;
        }

        // GET: api/LoansApplied
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanApplied>>> GetLoansApplied()
        {
            try
            {
                var result = await _service.GetAllLoansApplied();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/LoansApplied/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanApplied>> GetLoanAppliedById(int id)
        {
            try
            {
                var loanApplied = await _service.GetLoanAppliedById(id);
                if(loanApplied == null)
                {
                    return NotFound();
                }
                return loanApplied;
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        // GET: api/LoansApplied/5
        [HttpGet]
        [Route("byuserId/{userId}")]
        public async Task<ActionResult<IEnumerable<LoanApplied>>> GetLoanAppliedByUserId(string userId)
        {
            try
            {
                var loanApplied = await _service.GetAllLoansAppliedByUserId(userId);
                if (loanApplied == null)
                {
                    return NotFound();
                }
                return Ok(loanApplied);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // GET: api/LoansApplied/userName
        [HttpGet]
        [Route("byuserName/{userName}")]
        public async Task<ActionResult<IEnumerable<LoanApplied>>> GetLoanAppliedByUserName(string userName)
        {
            try
            {
                var loanApplied = await _service.GetAllLoansAppliedByUserName(userName);
                if (loanApplied == null)
                {
                    return NotFound();
                }
                return Ok(loanApplied);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // GET: api/LoansApplied/loanType
        [HttpGet]
        [Route("byloanType/{loanType}")]
        public async Task<ActionResult<IEnumerable<LoanApplied>>> GetLoanAppliedByLoanType(string loanType)
        {
            try
            {
                var loanApplied = await _service.GetAllLoansAppliedByLoanType(loanType);
                if (loanApplied == null)
                {
                    return NotFound();
                }
                return Ok(loanApplied);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        // PUT: api/LoansApplied/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanApplied(int id, LoanApplied loanApplied, string status)
        {
            try
            {
                bool result = await _service.UpdateLoanApplied(id, loanApplied,status);
                if (result)
                {
                    return Ok($"Loan with id :{id} has Successfuully updated");
                }
                return BadRequest("Update Request Failed");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/LoansApplied
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoanApplied>> PostLoanApplied(LoanApplied loanApplied)
        {
            Console.WriteLine("...........................................started");
            try
            {
                
                bool result = await _service.AddLoanApplied(loanApplied);
                if (result)
                {
                    Console.WriteLine("...end 11");
                    return CreatedAtAction("GetLoanAppliedById", new { id = loanApplied.LoanAppliedID }, loanApplied);
                }
                return BadRequest($"Loan type {loanApplied.LoanType} is Already Existing");
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        // DELETE: api/LoansApplied/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanApplied(int id)
        {
            try
            {
                await _service.DeleteLoanApplied(id);
                return Ok($"Loan with id :{id} has Deleted Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message );
            }
        }
        [HttpGet]
        [Route("updateloan")]
        public async Task<IActionResult> UpdateAccountBalance([FromQuery] int accountnumber, [FromQuery] int balance)
        {
            try
            {
                Console.WriteLine(accountnumber + " +" + balance + "...........");
                await _service.UpdateAccountBalance(accountnumber, balance);

                return Ok(true);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
