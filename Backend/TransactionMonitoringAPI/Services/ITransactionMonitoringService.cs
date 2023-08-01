using TransactionMonitoringAPI.Models;

namespace TransactionMonitoringAPI.Services
{
    public interface ITransactionMonitoringService
    {
        public Task<Transaction> AnalyzeTransaction(Transaction transaction);

        public Task<IEnumerable<Transaction>> GetTransactionHistory(int userId);

        public Task<bool> DetectSuspiciousActivities(Transaction transaction);

    }
}
