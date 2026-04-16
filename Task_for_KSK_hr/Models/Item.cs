namespace Task_for_KSK_hr.Models
{
    public enum Category
    {
        jewelry,
        clothes,
        food
    }
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
    }
}
