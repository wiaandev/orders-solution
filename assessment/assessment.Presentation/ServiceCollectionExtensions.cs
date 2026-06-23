using assessment.Application.Features.Customers;
using assessment.Application.Features.Orders;
using assessment.Infrastructure.Repositories;

namespace assessment.Presentation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}