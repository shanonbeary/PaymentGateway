namespace Checkout.AcquiringBank.Client;

public interface IAcquiringBankClient
{
    Task<AcquiringBankPaymentResponseDto> RequestPaymentAsync(AcquiringBankPaymentRequestDto paymentRequestDto);
}
