using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcountAPI.Models;

namespace AcountAPI.Data
{
    public class AccountDBContext : DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .Property(e => e.AccountNumber)
                .HasColumnType("bigint")
                .HasDefaultValueSql("NEXT VALUE FOR AccountNumberSequence");

            builder.HasSequence<long>("AccountNumberSequence")
                .StartsAt(2008908080808)
                .IncrementsBy(1);
        }

        public DbSet<Account> Accounts { get; set; } = default!;
    }
}
