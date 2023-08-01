using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using TransactionProcessingAPI.Models;
using TransactionProcessingAPI.Repository;

namespace TransactionProcessingAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7290/Account/byId/{transaction.UserId}");
            HttpResponseMessage responsereceive = await httpClient.GetAsync($"https://localhost:7290/Account/{transaction.ReceiverAccountNumber}");
            Console.WriteLine("......in method");
            if (response.IsSuccessStatusCode)
            {

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var jsonObject = JsonDocument.Parse(jsonResponse).RootElement;
                string accountNumber;
                
                if (transaction.SenderAccountNumber == "")
                {
                    Console.WriteLine("......empty");
                     accountNumber = jsonObject.GetProperty("accountNumber").ToString();
                }
                else
                {
                    Console.WriteLine("......loan");
                    accountNumber = transaction.SenderAccountNumber;
                }
                long balance = Convert.ToInt64( jsonObject.GetProperty("balance").ToString());

                // var user = JsonConvert.DeserializeObject<>(jsonResponse);
                string jsonResponse1 = await responsereceive.Content.ReadAsStringAsync();

                var jsonObject1 = JsonDocument.Parse(jsonResponse1).RootElement;
                string branchcodeReceiver = jsonObject1.GetProperty("branchCode").ToString();
                if (branchcodeReceiver.Equals(transaction.Ifsccode) && !accountNumber.Equals(transaction.ReceiverAccountNumber) && transaction.Amount<= balance) { 
                Transaction _transaction = new Transaction()
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = transaction.ReceiverAccountNumber,
                    TransactionDate = transaction.TransactionDate,
                    Status = transaction.Status,
                    UserId = transaction.UserId,
                    Ifsccode=transaction.Ifsccode,
                    Name = transaction.Name,
                };
                   

                return await _transactionRepository.CreateTransaction(_transaction);
                }
                else
                {
                    throw new TranscatiionException("Account and branch not matched or insuffiecnt balance");
                }
            }
           
            else
            {
                throw new TranscatiionException("Account Not found");
            }

        }

        public async Task DeleteTransaction(long id)
        {
            await _transactionRepository.DeleteTransaction(id);
        }

        public async Task<Transaction> GetTransaction(long id)
        {
            return await _transactionRepository.GetTransaction(id); 
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetTransactions();
        }
        

        public async Task UpdateTransaction(long transactionId, Transaction transaction)
        {
            await _transactionRepository.UpdateTransaction(transactionId, transaction);
        }
    }
}
