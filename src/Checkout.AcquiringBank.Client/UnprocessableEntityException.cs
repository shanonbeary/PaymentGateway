namespace Checkout.AcquiringBank.Client;

public class UnprocessableEntityException : Exception
{
    public AcquiringBankPaymentErrorResonseDto ErrorResponse { get; }

    public UnprocessableEntityException(AcquiringBankPaymentErrorResonseDto errorResponse)
    {
        ErrorResponse = errorResponse;
    }
}
