using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TransactionProcessingAPI.Data;
using TransactionProcessingAPI.Repository;
using TransactionProcessingAPI.Services;

namespace TransactionAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<TransactionContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
                    //?? throw new InvalidOperationException("Connection string not found.")),
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
            );

            // Add services to the container.
            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins("http://localhost:4200");
                    //builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<ITransactionService, TransactionService>();

            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

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

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}