using LibertyRustAcquiring.Models.Enums;

namespace LibertyRustAcquiring.DTOs
{
    public record GetPacksResponse(int Id, string Name, string Description, List<string> Image, List<decimal> Price, PackType Type);

}
