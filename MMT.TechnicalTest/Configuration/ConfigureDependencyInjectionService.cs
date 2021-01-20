using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Orders;
using Mmt.TechnicalTest.Repositories.Customers;
using Mmt.TechnicalTest.Repositories.Orders;
using Mmt.TechnicalTest.Services.Customers;

namespace MMT.TechnicalTest.Configuration
{
    public static class ConfigureDependencyInjectionService
    {
        /// <summary>
        /// Sets up instances for dependency injection.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="config">The configuration properties.</param>
        /// <exception cref="System.ArgumentNullException">
        /// services
        /// or
        /// config
        /// </exception>
        public static void Execute(IServiceCollection services, IConfiguration config)
        {
            if (services is null)
            {
                throw new System.ArgumentNullException(nameof(services));
            }

            if (config is null)
            {
                throw new System.ArgumentNullException(nameof(config));
            }

            services.Configure<CustomerApiConfig>(config.GetSection(CustomerApiConfig.CustomerApi));
            services.Configure<OrderDbConfig>(config.GetSection(OrderDbConfig.OrderDb));

            services.AddScoped<IOrderRepository, OrderDbRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemDbRepository>();
            services.AddScoped<ICustomerRepository, CustomerApiRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
