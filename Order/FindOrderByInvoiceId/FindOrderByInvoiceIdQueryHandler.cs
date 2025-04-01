using LibertyRustAcquiring.Data;
using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Order.FindOrderByInvoiceId
{
    public class FindOrderByInvoiceIdQueryHandler(ApplicationDbContext context) : IRequestHandler<FindOrderByInvoiceIdQuery, Models.Entities.Order>
    {        
        public async Task<Models.Entities.Order> Handle(FindOrderByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            var result = await context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.InvoiceId == request.InvoiceId);

            if(result is null)
            {
                throw new ObjectIsNullException<Models.Entities.Order>();
            }

            return result;
        }
    }
}
