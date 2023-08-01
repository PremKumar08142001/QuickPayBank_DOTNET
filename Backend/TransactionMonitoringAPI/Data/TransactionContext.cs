using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using TransactionMonitoringAPI.Models;

namespace TransactionMonitoringAPI.Data
{
    public class TransactionContext : DbContext
    {

        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
