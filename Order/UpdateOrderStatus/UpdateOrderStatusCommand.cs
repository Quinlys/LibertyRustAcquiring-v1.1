namespace LibertyRustAcquiring.Order.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<UpdateOrderStatusResult>
    {
        public Guid OrderId { get; }
        public string Status { get; }
        public UpdateOrderStatusCommand(Guid orderId, string status)
        {
            OrderId = orderId;
            Status = status;
        }
    }
}
