using Checkout.AcquiringBank.Client;
using Checkout.PaymentGateway.Model;
using Checkout.PaymentGateway.Repository;
using Checkout.PaymentGateway.Model.Entities;
using Checkout.PaymentGateway.Service.Utilities;

namespace Checkout.PaymentGateway.Service;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IAcquiringBankClient _acquiringBankClient;
    public PaymentService(IPaymentRepository paymentRepository, IAcquiringBankClient acquiringBank)
    {
        _paymentRepository = paymentRepository;
        _acquiringBankClient = acquiringBank;
    }

    public async Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto)
    {
        var acquiringBankPaymentRequest = new AcquiringBankPaymentRequestDto
        {
            CurrencyCode = paymentRequestDto.CurrencyCode,
            Amount = paymentRequestDto.Amount,
            Card = new AcquiringBankPaymentRequestDto.AcquiringBankCardRequestDto
            {
                Number = paymentRequestDto.Card.Number,
                Name = paymentRequestDto.Card.Name,
                ExpiryMonth = paymentRequestDto.Card.ExpiryMonth,
                ExpiryYear = paymentRequestDto.Card.ExpiryYear,
                CVV = paymentRequestDto.Card.CVV
            }
        };

        var acquiringBankPaymentResponse = await _acquiringBankClient.RequestPaymentAsync(acquiringBankPaymentRequest);

        if (acquiringBankPaymentResponse.Status == "Accepted")
        {
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

        throw new Exception();
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
                MaskedNumber = CardMaskUtility.MaskCardNumber(paymentEntity.Card.Number),
                Name = paymentEntity.Card.Name,
                ExpiryMonth = paymentEntity.Card.ExpiryMonth,
                ExpiryYear = paymentEntity.Card.ExpiryYear
            }
        };
    }
}
