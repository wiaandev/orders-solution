using assessment.Application.Features.Customers;
using assessment.Application.Features.Orders;
using assessment.Application.Features.Products;
using assessment.Application.Features.Payments;
using assessment.Application.Features.Mailing;
using assessment.Domain.Enum;
using assessment.Infrastructure.Bootstrap;
using assessment.Infrastructure.Database;
using assessment.Infrastructure.Keys;
using assessment.Infrastructure.Repositories;
using assessment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace assessment.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>(client =>
        {
            client.BaseAddress = new Uri("https://payment-gateway.local/");
        });
        
        services.AddDbContextPool<AppDbContext>(opts =>
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            opts.UseSqlServer(connectionString, sql =>
            {
                sql.MigrationsAssembly("assessment.Infrastructure");
                sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sql.CommandTimeout(600);
            });

            opts.EnableDetailedErrors();
            opts.EnableSensitiveDataLogging();
        });

        services.AddKeyedScoped<IPaymentService, CreditCardPaymentService>(PaymentKeys.CreditCard);
        services.AddKeyedScoped<IPaymentService, PayPalPaymentService>(PaymentKeys.Paypal);
        services.AddKeyedScoped<IPaymentService, PayPalPaymentService>(PaymentKeys.BankTransfer);
        services.AddScoped<Func<PayMethod, IPaymentService>>(sp =>
            paymentMethod =>
            {
                var paymentKey = paymentMethod switch
                {
                    PayMethod.CreditCard => PaymentKeys.CreditCard,
                    PayMethod.Paypal => PaymentKeys.Paypal,
                    PayMethod.BankTransfer => PaymentKeys.BankTransfer,
                    _ => throw new NotSupportedException($"Payment method '{paymentMethod}' is not configured.")
                };

                return sp.GetRequiredKeyedService<IPaymentService>(paymentKey);
            });
        services.AddScoped<IMailService, MailServices>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<Bootstrap.Bootstrap>();
        services.AddScoped<SeedService>();

        services.Configure<DropOptions>(configuration.GetSection("Drop"));
        services.Configure<MigrateOptions>(configuration.GetSection("Migrate"));
        services.Configure<SeedOptions>(configuration.GetSection("Seed"));
        
        

        return services;
    }
}


