using assessment.Domain.Enum;

namespace assessment.Application.Features.Payments;

public record PaymentProcessResult(bool IsSuccessful, PayMethod PaymentMethod);

