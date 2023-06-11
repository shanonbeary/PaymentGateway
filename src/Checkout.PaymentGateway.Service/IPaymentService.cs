using Checkout.PaymentGateway.Model.Dto;

namespace Checkout.PaymentGateway.Service;

public interface IPaymentService
{
    Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto);
    Task<PaymentDetailsResponseDto> GetPaymentByIdAsync(Guid paymentId);
}
