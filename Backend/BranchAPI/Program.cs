using BranchAPI.Data;
using BranchAPI.Repository;
using BranchAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BranchAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BranchDBContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("BranchDBContext") ?? throw new InvalidOperationException("Connection string 'CarWebApiContext' not found.")));
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IBranchService, BranchService>();
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