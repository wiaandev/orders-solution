using FluentValidation;

namespace assessment.Application.Features.Orders;

public class ProcessOrderValidator : AbstractValidator<ProcessOrderRequest>
{
    public ProcessOrderValidator()
    {
        this.RuleFor(p => p.PaymentMethod).IsInEnum();
        this.RuleFor(p => p.CustomerId).NotEmpty().WithMessage("customer ID cannot be empty");
    }
}