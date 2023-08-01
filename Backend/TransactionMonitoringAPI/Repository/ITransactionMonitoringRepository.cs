using TransactionMonitoringAPI.Models;

namespace TransactionMonitoringAPI.Repository
{
    public interface ITransactionMonitoringRepository
    {
        public Task<Transaction> AnalyzeTransaction(Transaction transaction);

        public Task<Transaction> GetTransactionData(int transactionId);

       // public bool DetectSuspiciousActivities(Transaction transaction);

    }
}
