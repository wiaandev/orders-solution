using assessment.Application.Features.Orders;
using Microsoft.AspNetCore.Mvc;

namespace assessment.Presentation.Endpoints;

public static class OrderEndpoints
{
     public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder endpoints)
     {

         endpoints.MapPost("/process-order", async (
             IOrderService orderService,
             [FromBody] ProcessOrderRequest message,
             CancellationToken ct) => await orderService.ProcessOrderAsync(message, ct));
         
         endpoints.MapGet("/order/{id}", async (
             int id,
             IOrderRepository orderRepository,
             CancellationToken ct) => await orderRepository.GetBydIdAsync(id, ct));

        return endpoints;
    }
}