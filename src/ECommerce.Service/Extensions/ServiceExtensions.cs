using System;
using ECommerce.Data;
using ECommerce.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Service
{
    public static class ServiceExtensions
    {
        public static void ConfigureContext(this IServiceCollection services)
        {
            var connectionString =
                Environment.GetEnvironmentVariable(Constants.ConnectionStringEnvironmentVariableName);

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception(
                    $"Connection string must be set in environment variables. Variable name: {Constants.ConnectionStringEnvironmentVariableName}");
            
            services.AddDbContext<ECommerceContext>(options =>
                options.UseNpgsql(connectionString));
            
            var serviceProvider = services.BuildServiceProvider();
            var db = serviceProvider.GetRequiredService<ECommerceContext>();
            db.Database.EnsureCreated();
            db.Database.Migrate();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}