namespace assessment.Application.Features.Payments;

public interface IPaymentService
{
    Task<PaymentProcessResult> ProcessPaymentAsync(decimal amountToBePaid, CancellationToken ct);
}