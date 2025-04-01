using LibertyRustAcquiring.Models.Enums;

namespace LibertyRustAcquiring.DTOs
{
    public record GetPackResponse(int Id, string Name, string Details, List<string> Image, List<decimal> Price, PackType Type);
}
