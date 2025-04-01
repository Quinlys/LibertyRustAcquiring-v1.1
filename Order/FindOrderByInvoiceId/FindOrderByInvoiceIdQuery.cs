namespace LibertyRustAcquiring.Order.FindOrderByInvoiceId
{
    public class FindOrderByInvoiceIdQuery : IRequest<Models.Entities.Order>
    {
        public string InvoiceId { get; }
        public FindOrderByInvoiceIdQuery(string invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
