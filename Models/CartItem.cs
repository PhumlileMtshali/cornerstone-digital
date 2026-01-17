namespace CornerstoneDigital.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        public string? CustomizationNotes { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;

        public virtual Cart Cart { get; set; }
        public virtual Service Service { get; set; }
    }
}
