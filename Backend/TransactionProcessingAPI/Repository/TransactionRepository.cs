using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionProcessingAPI.Data;
using TransactionProcessingAPI.Models;

namespace TransactionProcessingAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext _context;
        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            var _transaction = await _context.Transactions.AddAsync(transaction);
            
            await _context.SaveChangesAsync();

            return _transaction.Entity;
        }

        public async Task DeleteTransaction(long id)
        {
           
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction != null)
            {
                var deletedTransaction = _context.Transactions.Remove(transaction);

                await _context.SaveChangesAsync();

            } 

           
        }

        public async Task<Transaction> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();

            return transactions;
        }

        public async Task UpdateTransaction(long transactionId, Transaction transaction)
        {
            var _transaction = await _context.Transactions.FindAsync(transactionId);

            if(_transaction != null)
            {
               // _context.Transactions.Update(_transaction);

                _context.Entry(transaction).State = EntityState.Modified;

                await _context.SaveChangesAsync();

            }

        }
    }
}
