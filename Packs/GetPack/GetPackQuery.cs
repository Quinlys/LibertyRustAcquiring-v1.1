namespace LibertyRustAcquiring.Packs.GetPack
{
    public class GetPackQuery : IRequest<GetPackResponse>
    {
        public int Id { get; }
        public string Culture { get; }
        public GetPackQuery(int id, string culture)
        {
            Id = id;
            Culture = culture;
        }
    }
}
