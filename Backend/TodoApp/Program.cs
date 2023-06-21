using Microsoft.AspNetCore.Builder;

namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;
            services.ConfigureServices(builder.Configuration);
            var app = builder.Build();
            app.ConfigureMiddlewares();
        }
    }
}