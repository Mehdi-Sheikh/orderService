using OrderService.Entities;
using OrderService;
namespace OrderService.Contracts
{
    public interface IGeneratorFactory
    {
        /// <summary>
        /// This method generate a reciept
        /// </summary>
        /// <param name="type">type of reciept generator class for example Json</param>
        /// <returns></returns>
        public string Generate(Order order);
    }
}