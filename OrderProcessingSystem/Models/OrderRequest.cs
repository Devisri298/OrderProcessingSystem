namespace OrderProcessingSystem.Models
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItemList { get; set; }
    }
}
