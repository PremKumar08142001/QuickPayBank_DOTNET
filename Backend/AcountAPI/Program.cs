using AcountAPI.Data;
using AcountAPI.Repository;
using AcountAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AcountAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AccountDBContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDBContext") ?? throw new InvalidOperationException("Connection string 'CarWebApiContext' not found.")));
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    p =>
                    {
                        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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

            app.UseAuthorization();
            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}