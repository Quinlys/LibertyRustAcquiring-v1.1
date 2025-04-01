using System.Text.Json.Serialization;

namespace LibertyRustAcquiring.DTOs.Monobank
{
    public class InvoiceCreateResponse
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
        [JsonPropertyName("pageUrl")]
        public string PageUrl { get; set; }
    }
}
