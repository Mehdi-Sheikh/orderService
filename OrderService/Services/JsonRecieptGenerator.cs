using Newtonsoft.Json;
using OrderService.Contracts;
using OrderService.Entities;
namespace OrderService.Services
{
    public class JsonRecieptGenerator : IGeneratorFactory
    {
        public string Generate(Order order)
        {
            return JsonConvert.SerializeObject(order);
        }
    }
}