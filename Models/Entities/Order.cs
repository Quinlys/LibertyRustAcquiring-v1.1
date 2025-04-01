namespace LibertyRustAcquiring.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string InvoiceId { get; set; }
        public string SteamId { get; set; }
        public string Server { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public List<int> Packs { get; set; }
        public decimal Price { get; set; }
    }
}
