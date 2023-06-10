namespace Checkout.PaymentGateway.Service.Utilities;

public static class CardMaskUtility
{
    public static string MaskCardNumber(string cardNumber)
    {
        int lengthToMask = cardNumber.Length - 4;

        var parts = cardNumber.Split('-');

        var maskedParts = parts
            .Select((part, index) => index < parts.Length - 1 ? new string('*', part.Length) : part)
            .ToArray();

        return string.Join("-", maskedParts);
    }
}
