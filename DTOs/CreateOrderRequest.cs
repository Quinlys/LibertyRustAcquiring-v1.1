using System.Text.Json.Serialization;

namespace LibertyRustAcquiring.DTOs
{
    public class CreateOrderRequest
    {
        [JsonPropertyName("steamId")]
        public string SteamId { get; set; }

        [JsonPropertyName("server")]
        public string Server { get; set; }

        [JsonPropertyName("packs")]
        public string Packs { get; set; }
    }
}