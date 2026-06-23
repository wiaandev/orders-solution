using assessment.Application.Features.Orders;
using assessment.Application.Features.Customers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace assessment.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ProcessOrderValidator>();
        services.AddTransient<IValidator<ProcessOrderRequest>>(sp => sp.GetRequiredService<ProcessOrderValidator>());
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}