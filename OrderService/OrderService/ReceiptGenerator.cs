using OrderService;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
namespace OrderService
{

    public interface IGeneratorFactory
    {
        public string Generate(Order order);
    }
    public class HtmlRecieptGenerator : IGeneratorFactory
    {
        public string Generate(Order order)
        {
            var result = new StringBuilder($"<html><body><h1>Order receipt for '{order.Company}'</h1>");
            if (order._orderLines.Any())
            {
                result.Append("<ul>");
                foreach (var line in order._orderLines)
                {
                    result.Append(
                        $"<li>{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {line.DiscountedPrice:C}</li>");
                }
                result.Append("</ul>");
            }

            result.Append($"<h3>Subtotal: {order.TotalAmount:C}</h3>");
            result.Append($"<h3>MVA: {order.Tax:C}</h3>");
            result.Append($"<h2>Total: {order.TotalAmount + order.Tax:C}</h2>");
            result.Append("</body></html>");
            return result.ToString();
        }
    }

    public class JsonRecieptGenerator : IGeneratorFactory
    {
        public string Generate(Order order)
        {
            return JsonConvert.SerializeObject(order);
        }
    }

    public class Factory
    {
        public IGeneratorFactory FactoryClass(RecieptType type)
        {
            switch (type)
            {
                case RecieptType.Html:
                    return new HtmlRecieptGenerator();

                case RecieptType.Json:
                    return new JsonRecieptGenerator();
                default:
                    throw new System.Exception("The type of reciept is not supported");
            }
        }
        public enum RecieptType
        {
            Html = 1,
            Json = 2
        }
    }
}