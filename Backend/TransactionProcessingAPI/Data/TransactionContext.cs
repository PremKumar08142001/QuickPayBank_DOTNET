using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Principal;
using TransactionProcessingAPI.Models;

namespace TransactionProcessingAPI.Data
{
    public class TransactionContext : DbContext
    {

        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Transaction>()
                .Property(e => e.Id)
                .HasColumnType("long")
                .HasDefaultValueSql("CONVERT(INT, (ABS(CHECKSUM(NEWID())) % 900000) + 100000)");*/

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Id)
                .HasColumnType("bigint")
                .HasDefaultValueSql("NEXT VALUE FOR TransactionIdSequence");



            modelBuilder.HasSequence<long>("TransactionIdSequence")
                .StartsAt(20230107)
                .IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

    }
}
