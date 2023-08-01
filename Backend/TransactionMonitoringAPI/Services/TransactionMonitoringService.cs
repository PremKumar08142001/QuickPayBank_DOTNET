using Newtonsoft.Json;
using TransactionMonitoringAPI.Models;
using TransactionMonitoringAPI.Repository;

namespace TransactionMonitoringAPI.Services
{
    public class TransactionMonitoringService : ITransactionMonitoringService
    {
       /* private readonly ITransactionMonitoringRepository _transactionMonitoringRepository;

        public TransactionMonitoringService(ITransactionMonitoringRepository transactionMonitoringRepository)
        {
            _transactionMonitoringRepository = transactionMonitoringRepository;
        }*/

        public async Task<IEnumerable<Transaction>> GetTransactionHistory(int userId)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7127/Transaction");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                
                IEnumerable<Transaction> transactionHistory = JsonConvert.DeserializeObject<IEnumerable<Transaction>>(jsonResponse);

                return transactionHistory.Where(transaction => transaction.UserId == userId).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DetectSuspiciousActivities(Transaction transaction)
        {

            bool isFraudulent = false;

            var historicalTransactions = await GetTransactionHistory(transaction.UserId);

            decimal averageTransactionAmount = CalculateAverageTransactionAmount(historicalTransactions);

            if (transaction.Amount > averageTransactionAmount * 3 && averageTransactionAmount > 0 && historicalTransactions.Count() > 0)
            {
                isFraudulent = true;
            }

            return isFraudulent;
        }

        public decimal CalculateAverageTransactionAmount(IEnumerable<Transaction> transactions)
            {
                int transactionCount = transactions.Count();

                decimal totalAmount = transactions.Sum(t => t.Amount);

                if (transactionCount > 0)
                {
                    decimal averageAmount = totalAmount / transactionCount;
                    
                    return averageAmount;
                }
                else
                {
                    return 0; 
                }
            }


            public async Task<Transaction> AnalyzeTransaction(Transaction transaction)
            {
                var isFraudulent  = await DetectSuspiciousActivities(transaction);
                
                if (isFraudulent)
                {
                    Transaction _transaction = new Transaction()
                    {
                        Id = transaction.Id,
                        Amount = transaction.Amount,
                        SenderAccountNumber = transaction.SenderAccountNumber,
                        ReceiverAccountNumber = transaction.ReceiverAccountNumber,
                        TransactionDate = transaction.TransactionDate,
                        Status = TransactionStatus.Suspicious,
                        Ifsccode= transaction.Ifsccode,
                        Name=transaction.Name
                    };                    
                    return _transaction;
                }
                else
                {
                    return transaction;
                }

            }

          
        }
    }
