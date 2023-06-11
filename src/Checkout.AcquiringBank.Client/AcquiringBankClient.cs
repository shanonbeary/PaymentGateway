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

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<AcquiringBankPaymentResponseDto>();
    }
}
