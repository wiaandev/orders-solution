using assessment.Domain.Enum;

namespace assessment.Application.Features.Orders;

public record ProcessOrderRequest
{
    public required List<int> ProductIds { get; init; } = new();
    
    public required int CustomerId { get; init; }
    
    // Just passing the payment method here so I can validate it is the correct one so that it can fail early
    public PayMethod PaymentMethod { get; set; }

    public decimal Discount { get; init; }
}