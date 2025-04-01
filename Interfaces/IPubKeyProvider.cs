namespace LibertyRustAcquiring.Interfaces
{
    public interface IPubKeyProvider
    {
        Task<string> GetPublicKeyAsync();
    }
}
