using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionMonitoringAPI.Models;
using TransactionMonitoringAPI.Services;

namespace TransactionMonitoringAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionMonitoringController : ControllerBase
    {
        private readonly ITransactionMonitoringService _transactionMonitoringService;

        public TransactionMonitoringController(ITransactionMonitoringService transactionMonitoringService)
        {
            _transactionMonitoringService = transactionMonitoringService;
        }

        [HttpPost]
        [Route("analyze")]
        public async Task<ActionResult<Transaction>> AnalyzeTransaction([FromBody] Transaction transaction)
        {
           Transaction _transaction  = await _transactionMonitoringService.AnalyzeTransaction(transaction);
            if (_transaction == null)
            {
                return NotFound();
            }
            else
            {
                Transaction __transaction = new Transaction()
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    SenderAccountNumber = transaction.SenderAccountNumber,
                    ReceiverAccountNumber = transaction.ReceiverAccountNumber,
                    TransactionDate = transaction.TransactionDate,
                    Status = _transaction.Status,
                    Ifsccode = transaction.Ifsccode,
                    Name = transaction.Name
                };

                return Ok(__transaction);
            }
        }



    }
}
