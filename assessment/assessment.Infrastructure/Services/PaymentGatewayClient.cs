using System.Net;
using System.Net.Http.Json;
using assessment.Application.Features.Payments;

namespace assessment.Infrastructure.Services;

public class PaymentGatewayClient(HttpClient httpClient) : IPaymentGatewayClient
{
    public async Task<HttpStatusCode> ProcessPaymentAsync(decimal amountToBePaid, CancellationToken ct)
    {
        using var response = await httpClient.PostAsJsonAsync("payments", new { amountToBePaid }, ct);
        return response.StatusCode;
    }
}

