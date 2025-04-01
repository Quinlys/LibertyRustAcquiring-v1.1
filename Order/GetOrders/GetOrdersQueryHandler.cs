
using LibertyRustAcquiring.Data;
using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Order.GetOrders
{
    public class GetOrdersQueryHandler(ApplicationDbContext context) : IRequestHandler<GetOrdersQuery, List<Models.Entities.Order>>
    {
        public async Task<List<Models.Entities.Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await context.Orders
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
