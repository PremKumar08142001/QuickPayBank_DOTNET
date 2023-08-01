/*using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
using TransactionMonitoringAPI;
using TransactionMonitoringAPI.Models;
using TransactionMonitoringAPI.Data;
using System.Text;
using Newtonsoft.Json;

namespace TransactionMonitoringAPI.Repository
{
    public class TransactionMonitoringRepository : ITransactionMonitoringRepository
    {
       *//* private readonly TransactionContext _context;
        public TransactionMonitoringRepository(TransactionContext context)
        {
            _context = context;
        }*//*

        public async Task<Transaction> AnalyzeTransaction(Transaction transaction)
        {
            //var transactionData = await GetTransactionData(transaction.Id);

            var transactionData = transaction;

            bool isSuspicious = DetectSuspiciousActivities(transactionData);

            Transaction _transaction = new Transaction();

            if (isSuspicious)
            {
                TriggerAlert(transactionData);

                // _transaction =  await BlockTransaction(transactionData);
                _transaction.Status = TransactionStatus.Suspicious; 

                NotifyAuthorities(transactionData);   
            }
            else 
            {
                _transaction.Status = TransactionStatus.Completed;
            }

            return _transaction;
        }

        *//*public async Task<bool> DetectSuspiciousActivities(Transaction transaction)
        {

            bool isFraudulent = false;

            var historicalTransactions = await GetTransactionsHistory();

            decimal averageTransactionAmount = CalculateAverageTransactionAmount(historicalTransactions);

            if (transaction.Amount > averageTransactionAmount * 3)
            {
                isFraudulent = true;
            }

            return isFraudulent;
        }*//*

        
        public async Task<Transaction> GetTransactionData(int transactionId)
        {
           // Transaction transaction = await _context.Transactions.FindAsync(transactionId);
            
            return null;
        }

        private static void TriggerAlert(Transaction transaction)
        {
            string alertMessage = $"Suspicious transaction detected: Transaction ID {transaction.Id}";
            // AlertingService.SendAlert(alertMessage);
        }

        private async Task<Transaction> BlockTransaction(Transaction transaction)
        {
            transaction.Status = TransactionStatus.Blocked;
            return transaction;
            // await UpdateTransactionData(transaction);
        }

        private static void NotifyAuthorities(Transaction transaction)
        {
            string notificationMessage = $"Non-compliant transaction detected: Transaction ID {transaction.Id}";
            // NotificationService.Notify(notificationMessage);
        }


       *//* private async Task UpdateTransactionData(Transaction transaction)
        {
             _context.Transactions.Update(transaction);

            await _context.SaveChangesAsync();
        }*//*

        public async Task HandleSuspiciousTransaction(Transaction transaction)
        {
            // Trigger alert
            string alertMessage = $"Suspicious transaction detected. Transaction ID: {transaction.Id}";
           // await _alertingService.SendAlert(alertMessage);

            // Block transaction
           // await _blockingService.BlockTransaction(transaction);

            // Notify authorities
            string notificationMessage = $"Suspicious transaction detected. Transaction ID: {transaction.Id}";
           // await _notificationService.NotifyAuthorities(notificationMessage);
        }


    }
}
*/