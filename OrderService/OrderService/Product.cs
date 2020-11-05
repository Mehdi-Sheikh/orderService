namespace OrderService
{
    public class Product
    {
        public Product(string productType, string productName, int price)
        {
            ProductType = productType;
            ProductName = productName;
            Price = price;
        }

        public string ProductType { get; }
        public string ProductName { get; }
        public int Price { get; }

        public class Prices
        {
            public const int OneThousand = 1000;
            public const int TwoThousand = 2000;
            public const double TaxRate = .25d;
        }
    }
}