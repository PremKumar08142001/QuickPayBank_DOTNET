using TransactionProcessingAPI.Models;

namespace TransactionProcessingAPI.Services
{
    public interface ITransactionService
    {
        public Task<IEnumerable<Transaction>> GetTransactions();

        public Task<Transaction> GetTransaction(long id);

        public Task<Transaction> CreateTransaction(Transaction transaction);

        public Task UpdateTransaction(long transactionId, Transaction transaction);

        public Task DeleteTransaction(long id);

    }
}
