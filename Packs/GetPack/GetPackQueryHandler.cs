using LibertyRustAcquiring.Data;
using LibertyRustAcquiring.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Packs.GetPack
{
    public class GetPackQueryHandler(ApplicationDbContext context) : IRequestHandler<GetPackQuery, GetPackResponse>
    {
        public async Task<GetPackResponse> Handle(GetPackQuery request, CancellationToken cancellationToken)
        {
            var pack = await context.Packs
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new GetPackResponse(
                    x.Id,

                    request.Culture == "ua"
                        ? x.Name
                        : x.NameENG,

                    request.Culture == "ua"

                        ? x.Details
                        : x.DetailsENG,

                    x.Images,

                    new List<decimal> {x.SalePrice, x.Price },

                    x.Type)
                )
                .FirstAsync();

            if (pack is null) throw new ObjectIsNullException<Pack>();

            return pack;
        }
    }
}
