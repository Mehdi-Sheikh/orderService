using System.Linq;
using System.Text;
using OrderService.Contracts;
using OrderService.Entities;
using OrderService;

namespace OrderService.Services
{
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
            // result.Append($"<h2>Total: {order.TotalAmount + order.Tax:C}</h2>");
            result.Append($"<h2>Total: {order.TotalWithTax:C}</h2>");
            
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}