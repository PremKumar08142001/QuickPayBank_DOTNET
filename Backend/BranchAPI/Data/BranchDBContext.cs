using System.Collections.Generic;
using System.Security.Principal;
using BranchAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BranchAPI.Data
{
    public class BranchDBContext :DbContext
        {
            public BranchDBContext(DbContextOptions<BranchDBContext> options)
            : base(options)
            {
            }

            public DbSet<Branch> Branches { get; set; } = default!;
        }
    }

