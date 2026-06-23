using assessment.Application.Features.Customers;
using assessment.Application.Features.Mailing;
using assessment.Application.Features.Payments;
using assessment.Application.Features.Products;
using assessment.Domain.Enum;
using FluentValidation;

namespace assessment.Application.Features.Orders;

public class OrderService(
    IProductRepository productRepository,
    Func<PayMethod, IPaymentService> paymentServiceResolver,
    IMailService mailService,
    ICustomerRepository customerRepository,
    ProcessOrderValidator orderValidator) : IOrderService
{
    public async Task<ProcessOrderResult> ProcessOrderAsync(ProcessOrderRequest request, CancellationToken ct)
    {
        await orderValidator.ValidateAndThrowAsync(request, ct);

        var products = await productRepository.GetProductsByIdAsync(request.ProductIds, ct);
        var total = products.Sum(x => x.Price);

        if (request.Discount > 0)
        {
            total = total - request.Discount;
        }

        var paymentService = paymentServiceResolver(request.PaymentMethod);
        var paymentResult = await paymentService.ProcessPaymentAsync(total, ct);
        var isMailSent = false;

        if (paymentResult.IsSuccessful)
        {
            var customer = await customerRepository.GetByIdAsync(request.CustomerId, ct);
            if (customer is null)
            {
                throw new ApplicationException("Customer cannot be found");
            }

            isMailSent = await mailService.SendMail(
                customer.Email);
        }

        return new ProcessOrderResult(paymentResult.IsSuccessful, paymentResult.PaymentMethod, isMailSent);
    }

}