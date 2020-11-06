using OrderService.Contracts;

namespace OrderService.Services
{
    public class Factory
    {
        /// <summary>
        /// This method use a Initialize a reciepptGenerator base on type
        /// </summary>
        /// <param name="type">type of reciept generator class for example Json</param>
        /// <returns></returns>
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