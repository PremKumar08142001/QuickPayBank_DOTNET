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
    [Route("[controller]")]
    [ApiController]
    public class LoansOfferedController : ControllerBase
    {
        private readonly ILoanOfferedService _service;

        public LoansOfferedController(ILoanOfferedService service)
        {
            _service = service;
        }

        // GET: api/LoansOffered
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanOffered>>> GetLoansOffered()
        {
            try
            {
                var result = await _service.GetAllLoansOffered();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/LoansOffered/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanOffered>> GetLoanOfferedById(int id)
        {
            var loanOffered = await _service.GetLoanById(id);

            if (loanOffered == null)
            {
                return NotFound();
            }

            return loanOffered;
        }
        //GET: api/LoansOffered/Home
        [HttpGet]
        [Route("byloanType/{loanType}")]
        public async Task<ActionResult<LoanOffered>> GetLoanOfferedByLoanType(string loanType)
        {
            var loanOffered = await _service.GetLoanByLoanType(loanType);

            if (loanOffered == null)
            {
                return NotFound();
            }

            return loanOffered;
        }

        // PUT: api/LoansOffered/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanOffered(int id, LoanOffered loanOffered)
        {
            try
            {
                bool result = await _service.UpdateLoanOffered(id, loanOffered);
                if (result)
                {
                    return Ok($"Loan with id :{id} has Successfuully updated");
                }
                return BadRequest("Update Request Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/LoansOffered
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoanOffered>> PostLoanOffered(LoanOffered loanOffered)
        {
            try
            {
                bool result = await _service.AddLoanOffered(loanOffered);
                if(result)
                {
                    return CreatedAtAction("GetLoanOfferedById", new { id = loanOffered.LoanOfferedID }, loanOffered);
                }
                else { return BadRequest($"Loan {loanOffered.LoanType} Already Exist"); }
                
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // DELETE: api/LoansOffered/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanOffered(int id)
        {
            try
            {
                await _service.DeleteLoanOffered(id);
                return Ok($"Loan with id :{id} has Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }
    }
}
