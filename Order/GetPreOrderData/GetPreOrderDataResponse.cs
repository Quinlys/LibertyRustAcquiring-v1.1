namespace LibertyRustAcquiring.Order.GetOrderData
{
    public record GetPreOrderDataResponse(int TotalItems, decimal TotalPrice, bool CanBeCreated, string ErrorCaused);
}
