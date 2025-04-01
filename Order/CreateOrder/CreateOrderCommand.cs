using LibertyRustAcquiring.Models.Constants;

namespace LibertyRustAcquiring.Order.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResult>
    {
        public string SteamId { get; }
        public string Server { get; }
        public string Status { get; }
        public string InvoiceId { get; }
        public List<int> Packs { get; }

        public CreateOrderCommand(string steamId, string server, string invoiceId, List<int> packs)
        {
            SteamId = steamId;
            Server = server;
            Packs = packs;
            InvoiceId = invoiceId;
            Status = OrderStatuses.Created;
        }
    }
}