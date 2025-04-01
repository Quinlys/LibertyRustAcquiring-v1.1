using LibertyRustAcquiring.Data;
using LibertyRustAcquiring.Interfaces;
using LibertyRustAcquiring.Settings;
using Microsoft.EntityFrameworkCore;
using LibertyRustAcquiring.Models.Enums;
using LibertyRustAcquiring.Utils;

namespace LibertyRustAcquiring.Order.CreateOrder
{
    public class CreateOrderCommandHandler(
        ApplicationDbContext context,
        ILogger<CreateOrderCommandHandler> logger) : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await context.Database.BeginTransactionAsync();

            try
            {
                var groupedPacks = request.Packs
                .GroupBy(id => id)
                .Select(g => new { PackId = g.Key, Quantity = g.Count(), })
                .ToList();

                var packs = await context.Packs
                    .Where(p => request.Packs.Contains(p.Id))
                    .Include(x => x.Items)
                    .ToListAsync(cancellationToken);

                decimal totalPrice = groupedPacks.Sum(g =>
                {
                    var pack = packs.FirstOrDefault(p => p.Id == g.PackId);

                    if (pack == null)
                    {
                        throw new KeyNotFoundException($"Pack with an Id {g.PackId} was not found.");
                    }

                    return pack.Price * g.Quantity;
                });

                var order = new Models.Entities.Order
                {
                    Id = Guid.NewGuid(),
                    Server = request.Server,
                    SteamId = request.SteamId,
                    CreatedAt = DateTime.UtcNow,
                    InvoiceId = request.InvoiceId,
                    Status = request.Status,
                    Price = totalPrice,
                    Packs = request.Packs
                };

                await context.Orders.AddAsync(order);

                await context.SaveChangesAsync();

                await context.Database.CurrentTransaction!.CommitAsync();               

                return new CreateOrderResult(true);
            }
            catch (Exception ex) 
            {
                logger.LogError("[CreateOrderCommand] An exception occured: {ex}", ex.Message);
                await context.Database.CurrentTransaction!.RollbackAsync();
                return new CreateOrderResult(false);
            }
        }
    }
}