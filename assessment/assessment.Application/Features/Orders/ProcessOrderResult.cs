using assessment.Domain.Enum;

namespace assessment.Application.Features.Orders;

public record ProcessOrderResult(bool IsSuccessful, PayMethod PaymentMethod, bool isMailSent);

