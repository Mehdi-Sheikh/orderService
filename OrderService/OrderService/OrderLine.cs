namespace OrderService
{
    public class OrderLine
    {
        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            DiscountedPrice=product.Price*quantity;
        }

        public double DiscountedPrice { get; set;}
        public Product Product { get; }
        public int Quantity { get; }
    }
}