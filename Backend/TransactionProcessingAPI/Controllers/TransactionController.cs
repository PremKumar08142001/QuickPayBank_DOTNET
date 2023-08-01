using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Principal;
using System.Text;
using TransactionAPI.Models;
using TransactionProcessingAPI.Data;
using TransactionProcessingAPI.Models;
using TransactionProcessingAPI.Services;

namespace TransactionProcessingAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

         [HttpPost]
         [Route("analyze")]
       // public async Task<ActionResult<Transaction>> IsTransactionSuspicious(Transaction transaction)
        public async Task<Transaction> IsTransactionSuspicious(Transaction transaction)
        {
            
            using var client = new HttpClient();

            var transactionJson = JsonConvert.SerializeObject(transaction);

            var _transaction = new StringContent(transactionJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"https://localhost:7064/TransactionMonitoring/analyze", _transaction);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            Transaction transactionResponse = JsonConvert.DeserializeObject<Transaction>(jsonResponse);

         //   return Ok(transactionResponse);
            return transactionResponse;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            //var transactions = await _context.Transactions.ToListAsync();
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }
        
        [HttpGet("suspicious")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetSuspiciousTransactions()
        {
            var transactions = await _transactionService.GetTransactions();

            transactions = transactions.Where(transaction => transaction.Status == TransactionStatus.Suspicious);

            return Ok(transactions);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Transaction>> GetTransaction(long id)
        {
           // var transaction = await _context.Transactions.FindAsync(id);
            var transaction = await _transactionService.GetTransaction(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(Transaction transaction)
        {
            try
            {
                Console.WriteLine("in controller");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var _transaction = transaction;

                transaction.TransactionDate = DateTime.Now;

                transaction.Status = TransactionStatus.Completed;

                var result = await IsTransactionSuspicious(transaction);

                if (result != null)
                {
                    if (result.Ifsccode != null) { 
                    Transaction _result = new Transaction
                    {
                        Amount = result.Amount,
                        SenderAccountNumber = result.SenderAccountNumber,
                        ReceiverAccountNumber = result.ReceiverAccountNumber,
                        TransactionDate = DateTime.Now,
                        Status = result.Status,
                        UserId = transaction.UserId,
                        Ifsccode = transaction.Ifsccode,
                        Name = transaction.Name
                    };

                    _transaction = await _transactionService.CreateTransaction(_result);
                     return CreatedAtAction(nameof(GetTransaction), new { id = _transaction.Id }, _transaction);

                    }

                }
                return NotFound("Ifsccode and acount does not matched");
               
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(long id, Transaction transaction)
        {
           
            try
            {
                await _transactionService.UpdateTransaction(id, transaction);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _transactionService.GetTransaction(id) is null)
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            // var transaction = await _context.Transactions.FindAsync(id);
            var transaction = await _transactionService.GetTransaction(id);
            
            if (transaction == null)
            {
                return NotFound();
            }

            else
            {
                await _transactionService.DeleteTransaction(id);
          
                return Ok();
            }
        }

        
    }
}
