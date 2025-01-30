namespace OrderProcessingSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        // Total Price Calculation
        public decimal TotalPrice => OrderItems != null ? (OrderItems.Sum(item => item.Product != null ? (item.Product.Price * item.Quantity) : 0)) : 0;

        public bool IsFulfilled { get; set; }
    }
}
