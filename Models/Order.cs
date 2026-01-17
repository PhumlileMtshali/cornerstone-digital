namespace CornerstoneDigital.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        public string? ProjectDetails { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Service Service { get; set; }
    }
}