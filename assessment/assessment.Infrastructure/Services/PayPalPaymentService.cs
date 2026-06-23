using System.Net;
using assessment.Application.Features.Payments;
using assessment.Domain.Enum;

namespace assessment.Infrastructure.Services;

public class PayPalPaymentService(IPaymentGatewayClient paymentGatewayClient) : IPaymentService
{
    public async Task<PaymentProcessResult> ProcessPaymentAsync(decimal amountToBePaid, CancellationToken ct)
    {
        var statusCode = await paymentGatewayClient.ProcessPaymentAsync(amountToBePaid, ct);
        return new PaymentProcessResult(statusCode == HttpStatusCode.OK, PayMethod.Paypal);
    }
}