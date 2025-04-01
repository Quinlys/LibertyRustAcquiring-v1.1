using LibertyRustAcquiring.Data;
using LibertyRustAcquiring.Models.Enums;
using LibertyRustAcquiring.Settings;
using LibertyRustAcquiring.Utils;
using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Order.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler(
        ApplicationDbContext context,
        IConfiguration configuration,
        IServerConnection connection,
        ILogger<UpdateOrderStatusCommandHandler> logger)  : IRequestHandler<UpdateOrderStatusCommand, UpdateOrderStatusResult>
    {
        public async Task<UpdateOrderStatusResult> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            await context.Database.BeginTransactionAsync();

            try
            {
                await context.Orders
                    .Where(x => x.Id == request.OrderId)
                    .ExecuteUpdateAsync(o =>
                        o.SetProperty(x => x.Status, request.Status));

                await context.Database.CurrentTransaction!.CommitAsync();

                if (request.Status == "success")
                {
                    await SendPacks(request.OrderId, cancellationToken);
                    logger.LogInformation("[UpdateOrderStatusCommandHandler] Sending packs. OrderId: {orderId}", request.OrderId);
                }

                logger.LogInformation("[UpdateOrderStatusCommandHandler] Successfully updated status on {status} for order: {orderId}", request.Status, request.OrderId);
                return new UpdateOrderStatusResult(true);
            }
            catch (Exception ex) 
            {
                logger.LogError("[UpdateOrderStatusCommandHandler] An exception occured: {ex}", ex.Message);
                await context.Database.CurrentTransaction!.RollbackAsync();
                return new UpdateOrderStatusResult(false);
            }
        }
        private async Task SendPacks(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null)
            {
                throw new KeyNotFoundException($"Order with an Id {orderId} was not found.");
            }

            var groupedPacks = order.Packs
                .GroupBy(id => id)
                .Select(g => new { PackId = g.Key, Quantity = g.Count(), })
                .ToList();

            var packs = await context.Packs
                .Where(p => order.Packs.Contains(p.Id))
                .Include(x => x.Items)
                .ToListAsync(cancellationToken);

            var server = new ServerInfo
            {
                Hostname = configuration[$"{order.Server}:Ip"]!,
                RconPort = configuration[$"{order.Server}:Port"]!,
                RconPassword = configuration[$"{order.Server}:Password"]!,
            };

            foreach (var group in groupedPacks)
            {
                var pack = packs.FirstOrDefault(p => p.Id == group.PackId);
                if (pack is null || pack.Items is null)
                {
                    throw new ObjectIsNullException<Pack>();
                }

                string command = string.Empty;
                foreach (var item in pack.Items)
                {
                    switch (item.ItemType)
                    {
                        case ItemType.Resource:
                            command = RustCommands.AddItemCommand(order.SteamId, item.Name, item.Quantity * group.Quantity);
                            break;
                        case ItemType.Privilege:
                            command = RustCommands.AddPrivelegeCommand(order.SteamId, item.Name, item.Quantity * group.Quantity);
                            break;
                        case ItemType.Skins:
                            command = RustCommands.UnclockSkinsCommand(order.SteamId, 30 * group.Quantity);
                            break;
                        case ItemType.Blueprints:
                            command = RustCommands.UnlockBlueprints(order.SteamId);
                            break;
                        default:
                            break;
                    }
                    var result = await connection.SendCommand(server, command);

                    logger.LogInformation("OrderId: {orderId}. Executed command: {command}. Result: {result}", orderId, command, result);
                }
            }
        }
    }

}
