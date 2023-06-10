using Checkout.PaymentGateway.Model;

namespace Checkout.PaymentGateway.Service;

public interface IPaymentService
{
    Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto);
    Task<PaymentDetailsResponseDto> GetPaymentByIdAsync(Guid paymentId);
}
