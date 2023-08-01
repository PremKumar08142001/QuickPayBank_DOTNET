using TransactionProcessingAPI.Models;

namespace TransactionProcessingAPI.Repository
{
    public interface ITransactionRepository
    {
        public Task<IEnumerable<Transaction>> GetTransactions();

        public Task<Transaction> GetTransaction(long id);

        public Task<Transaction> CreateTransaction(Transaction transaction);

        public Task UpdateTransaction(long transactionId, Transaction transaction);

        public Task DeleteTransaction(long id);
    }
}
