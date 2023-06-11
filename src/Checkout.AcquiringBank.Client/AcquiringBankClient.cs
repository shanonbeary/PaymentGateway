using System.Net.Http.Json;

namespace Checkout.AcquiringBank.Client;

public class AcquiringBankClient : IAcquiringBankClient
{
    private readonly HttpClient _httpClient;

    public AcquiringBankClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AcquiringBankPaymentResponseDto> RequestPaymentAsync(AcquiringBankPaymentRequestDto paymentRequestDto)
    {
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            "payments",
            paymentRequestDto);

        return response.StatusCode switch
        {
            System.Net.HttpStatusCode.Created => await response.Content.ReadFromJsonAsync<AcquiringBankPaymentResponseDto>(),
            System.Net.HttpStatusCode.UnprocessableEntity => throw new UnprocessableEntityException(await response.Content.ReadFromJsonAsync<AcquiringBankPaymentErrorResonseDto>()),
            _ => throw new UnexpectedStatusCodeException(response.StatusCode, await response.Content.ReadAsStringAsync())
        };
    }
}
