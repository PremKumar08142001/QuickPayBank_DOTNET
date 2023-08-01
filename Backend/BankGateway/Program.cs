using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace BankGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("ocelot.json").Build();
            builder.Services.AddOcelot(configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    p =>
                    {
                        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseCors("MyPolicy");
            app.UseOcelot().Wait();
            // app.MapRazorPages();
            app.Run();
        }
    }
}