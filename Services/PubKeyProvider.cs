using LibertyRustAcquiring.Interfaces;

namespace LibertyRustAcquiring.Utils
{
    public class PubKeyProvider : IPubKeyProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PubKeyProvider> _logger;
        private string _cachedPublicKey;
        private DateTime _lastFetched;

        public PubKeyProvider(IHttpClientFactory httpClientFactory, ILogger<PubKeyProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _lastFetched = default;
        }

        public async Task<string> GetPublicKeyAsync()
        {
            if (string.IsNullOrEmpty(_cachedPublicKey) || DateTime.UtcNow - _lastFetched > TimeSpan.FromHours(24))
            {
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.GetAsync("https://api.monobank.ua/api/merchant/pubkey");
                    if (response.IsSuccessStatusCode)
                    {
                        _cachedPublicKey = await response.Content.ReadAsStringAsync();
                        _lastFetched = DateTime.UtcNow;
                        _logger.LogInformation("Successfully fetched Monobank public key.");
                    }
                    else
                    {
                        _logger.LogError("Failed to fetch Monobank public key. Status code: {StatusCode}", response.StatusCode);
                        throw new Exception("Failed to fetch Monobank public key.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception while fetching Monobank public key.");
                    throw;
                }
            }

            return _cachedPublicKey;
        }
    }
}
