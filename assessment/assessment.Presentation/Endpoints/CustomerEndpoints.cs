using assessment.Application.Features.Customers;
using assessment.Application.Features.Orders;
using Microsoft.AspNetCore.Mvc;

namespace assessment.Presentation.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        
        endpoints.MapGet("/customer/{id}/orders", async (
            int id,
            ICustomerService customerService,
            CancellationToken ct) => await customerService.GetOrders(id, ct));

        return endpoints;
    }
}