using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanAPI.Models;

namespace LoanAPI.Data
{
    public class LoanAPIContext : DbContext
    {
        public LoanAPIContext (DbContextOptions<LoanAPIContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<LoanApplied>()
                .Property(e => e.LoanAppliedID)
                .HasColumnType("bigint")
                .HasDefaultValueSql("NEXT VALUE FOR AccountNumberSequence");

            builder.HasSequence<long>("AccountNumberSequence")
                .StartsAt(20089080)
                .IncrementsBy(1);
        }

    public DbSet<LoanAPI.Models.LoanApplied> LoansApplied { get; set; } = default!;

        
        public DbSet<LoanAPI.Models.LoanOffered> LoansOffered { get; set; }
    }
}
