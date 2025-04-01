using System.Text.Json.Serialization;

namespace LibertyRustAcquiring.DTOs
{
    public class GetPreOrderResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("notify")]
        public string Notify { get; set; }
    }
}
