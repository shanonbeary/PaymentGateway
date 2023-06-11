using System.Net;

namespace Checkout.AcquiringBank.Client;

public class UnexpectedStatusCodeException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ResponseContent { get; }

    public UnexpectedStatusCodeException(HttpStatusCode statusCode, string responseContent)
        : base($"Unexpected status code: {statusCode}. Response content: {responseContent}")
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }
}
