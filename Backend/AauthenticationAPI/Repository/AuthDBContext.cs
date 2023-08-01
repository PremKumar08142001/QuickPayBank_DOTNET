using System.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace AuthenticationAPI.Repository
{
    public class AuthDBContext  : IdentityDbContext<User>
    {
            public AuthDBContext(DbContextOptions<AuthDBContext> options): base(options)
            { }
        protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);


            builder.Entity<User>()
                .Property(e => e.UserGender)
            .HasMaxLength(250);
            
            builder.Entity<User>()
                .Property(e => e.UserAddress)
                .HasMaxLength(250);
            builder.Entity<User>()
    .Property(e => e.UserId)
    .HasColumnType("int")
    .HasMaxLength(250);
        }

        }
    }
