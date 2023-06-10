using Checkout.PaymentGateway.Model;
using Checkout.PaymentGateway.Repository;
using Checkout.PaymentGateway.Repository.Entities;
using Checkout.PaymentGateway.Service.Utilities;

namespace Checkout.PaymentGateway.Service;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto)
    {
        // Call Acquiring Bank

        var paymentEntity = new PaymentEntity
        {
            Id = Guid.NewGuid(),
            Amount = paymentRequestDto.Amount,
            CurrencyCode = paymentRequestDto.CurrencyCode,
            Status = "Successful",
            Card = new CardEntity
            {
                Id = Guid.NewGuid(),
                Number = paymentRequestDto.Card.Number,
                ExpiryMonth = paymentRequestDto.Card.ExpiryMonth,
                ExpiryYear = paymentRequestDto.Card.ExpiryYear,
                Name = paymentRequestDto.Card.Name,
                CVV = paymentRequestDto.Card.CVV
            }
        };

        await _paymentRepository.CreatePaymentAsync(paymentEntity);

        return new PaymentResponseDto
        {
            Id = paymentEntity.Id,
            Status = paymentEntity.Status
        };
    }

    public async Task<PaymentDetailsResponseDto> GetPaymentByIdAsync(Guid paymentId)
    {
        var paymentEntity = await _paymentRepository.GetPaymentByIdAsync(paymentId) ?? throw new EntityNotFoundException();

        return new PaymentDetailsResponseDto
        {
            Id = paymentEntity.Id,
            Status = paymentEntity.Status,
            CurrencyCode = paymentEntity.CurrencyCode,
            Amount = paymentEntity.Amount,
            Card = new PaymentDetailsResponseDto.CardDetailsResponseDto
            {
                Number = CardMaskUtility.MaskCardNumber(paymentEntity.Card.Number),
                Name = paymentEntity.Card.Name,
                ExpiryMonth = paymentEntity.Card.ExpiryMonth,
                ExpiryYear = paymentEntity.Card.ExpiryYear,
                CVV = paymentEntity.Card.CVV
            }
        };
    }
}
