namespace LibertyRustAcquiring.Packs.GetPacks
{
    public class GetPacksQuery : IRequest<List<GetPacksResponse>>
    {
        public string? Culture { get; }
        public GetPacksQuery(string? culture)
        {
            Culture = culture;
        }
    }
}
