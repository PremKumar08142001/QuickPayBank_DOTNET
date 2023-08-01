using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LoanAPI.Data;
using LoanAPI.Repository;
using LoanAPI.Service;

namespace LoanAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<LoanAPIContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LoanAPIContext") ?? throw new InvalidOperationException("Connection string 'LoanAPIContext' not found.")));

            // Add services to the container.

            builder.Services.AddScoped<ILoanAppliedRepository, LoanAppliedRepository>();
            builder.Services.AddScoped<ILoanOfferedRepository, LoanOfferedRepository>();

            builder.Services.AddScoped<ILoanAppliedService, LoanAppliedService>();
            builder.Services.AddScoped<ILoanOfferedService, LoanOfferedService>();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("corsPolicy", p =>
                {
                    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("corsPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}