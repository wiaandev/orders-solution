namespace assessment.Application.Features.Orders;

public interface IOrderService
{
    Task<ProcessOrderResult> ProcessOrderAsync(ProcessOrderRequest req, CancellationToken ct = default);
}