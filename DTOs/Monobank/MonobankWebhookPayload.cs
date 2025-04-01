using System.Text.Json.Serialization;

namespace LibertyRustAcquiring.DTOs.Monobank
{
    public class MonobankWebhookPayload
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("amount")]
        public long Amount { get; set; }

        [JsonPropertyName("ccy")]
        public int Ccy { get; set; }
        [JsonPropertyName("validity")]
        public int Validity { get; set; }
    }
}
