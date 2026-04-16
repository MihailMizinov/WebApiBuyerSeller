namespace Task_for_KSK_hr.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BuyerId { get; set; }
        public DateTime PurchasedAt { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Amount { get; set; }

    }
}
