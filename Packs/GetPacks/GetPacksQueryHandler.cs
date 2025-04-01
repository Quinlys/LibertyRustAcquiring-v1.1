using LibertyRustAcquiring.Data;
using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Packs.GetPacks
{
    public class GetPacksQueryHandler(ApplicationDbContext context) : IRequestHandler<GetPacksQuery, List<GetPacksResponse>>
    {
        public async Task<List<GetPacksResponse>> Handle(GetPacksQuery request, CancellationToken cancellationToken)
        {
            var packs = await context.Packs
                .AsNoTracking()
                .Include(x => x.Items)
                .Select(x => new GetPacksResponse(
                    x.Id,

                    request.Culture == "ua" 
                        ? x.Name 
                        : x.NameENG,

                    request.Culture == "ua"
                        ? x.Description
                        : x.DescriptionENG,

                    x.Images,

                    new List<decimal> { x.SalePrice, x.Price },

                    x.Type)
                )
                .ToListAsync();

            return packs;
        }
    }
}
