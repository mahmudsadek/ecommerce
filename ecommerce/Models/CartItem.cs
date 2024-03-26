namespace ecommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public  Product Product { get; set; }

        public int CarttId { get; set; }
        public  Cart cart { get; set; }
    }
}
