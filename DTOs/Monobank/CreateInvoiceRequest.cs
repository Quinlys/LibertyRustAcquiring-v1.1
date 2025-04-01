using System.Text.Json.Serialization;

namespace LibertyRustAcquiring.DTOs.Monobank
{
    public class CreateInvoiceRequest
    {
        [JsonPropertyName("amount")]
        public long Amount { get; set; }
        [JsonPropertyName("ccy")]
        public int Ccy { get; set; }
        [JsonPropertyName("validity")]
        public long Validaty { get; set; }
        [JsonPropertyName("paymentType")]
        public string PaymentType { get; set; }
        [JsonPropertyName("redirectUrl")]
        public string RedirectUrl { get; set; }
        [JsonPropertyName("webHookUrl")]
        public string WebhookUrl { get; set; }
    }
}
