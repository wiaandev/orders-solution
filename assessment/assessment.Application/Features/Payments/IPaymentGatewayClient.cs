using System.Net;

namespace assessment.Application.Features.Payments;

public interface IPaymentGatewayClient
{
    Task<HttpStatusCode> ProcessPaymentAsync(decimal amountToBePaid, CancellationToken ct);
}

