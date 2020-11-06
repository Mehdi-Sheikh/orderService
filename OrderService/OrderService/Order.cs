using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
// using OrderService.Services;

namespace OrderService
{
    public class Order
    {
        public readonly List<OrderLine> _orderLines = new List<OrderLine>();

        public double TotalAmount
        {
            get { return _orderLines.Sum(x => x.DiscountedPrice) - Tax; }//calculate sum of orderlines when discount has been calculated
            set { if (value < 0) TotalAmount = 0; }//prevent to be negative number
        }
        public double Tax { get { return TotalAmount * _taxRate; } set { Tax = value; } }
        private double _taxRate;
        private int _numeralDiscountRate;
        private int _numeralDiscountNumber;
        private double _flatDiscount;
        public string Company { get; set; }

        public Order(string company, double taxRate, double flatDiscount,
         int numeralDiscountRate, int numeralDiscountNumber)
        {
            Company = company;
            _taxRate = taxRate;
            _flatDiscount = flatDiscount;
            _numeralDiscountRate = numeralDiscountRate;
            _numeralDiscountNumber = numeralDiscountNumber;
        }
        /// <summary>
        /// Add some orders order line and then calculate flat and numerical discount 
        /// After calculation every thing is ok to generate receipt
        /// </summary>
        /// <param name="orderLines">A list of orderlines</param>
        public void AddLines(List<OrderLine> orderLines)
        {
            _orderLines=orderLines;
            // _orderLines.AddRange(orderLines);
            Calculate();
        }

        // public string GenerateReceipt()
        // {
        //     var totalAmount = 0d;
        //     var result = new StringBuilder($"Order receipt for '{Company}'{Environment.NewLine}");
        //     foreach (var line in _orderLines)
        //     {
        //         var thisAmount = 0d;
        //         switch (line.Product.Price)
        //         {
        //             case Product.Prices.OneThousand:
        //                 if (line.Quantity >= 5)
        //                     thisAmount += line.Quantity * line.Product.Price * .9d;
        //                 else
        //                     thisAmount += line.Quantity * line.Product.Price;
        //                 break;
        //             case Product.Prices.TwoThousand:
        //                 if (line.Quantity >= 3)
        //                     thisAmount += line.Quantity * line.Product.Price * .8d;
        //                 else
        //                     thisAmount += line.Quantity * line.Product.Price;
        //                 break;
        //         }

        //         result.AppendLine(
        //             $"\t{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}");
        //         totalAmount += thisAmount;
        //     }

        //     result.AppendLine($"Subtotal: {totalAmount:C}");
        //     var totalTax = totalAmount * Product.Prices.TaxRate;
        //     result.AppendLine($"MVA: {totalTax:C}");
        //     result.Append($"Total: {totalAmount + totalTax:C}");
        //     return result.ToString();
        // }

        // public string GenerateHtmlReceipt()
        // {
        //     var totalAmount = 0d;
        //     var result = new StringBuilder($"<html><body><h1>Order receipt for '{Company}'</h1>");
        //     if (_orderLines.Any())
        //     {
        //         result.Append("<ul>");
        //         foreach (var line in _orderLines)
        //         {
        //             var thisAmount = 0d;
        //             switch (line.Product.Price)
        //             {
        //                 case Product.Prices.OneThousand:
        //                     if (line.Quantity >= 5)
        //                         thisAmount += line.Quantity * line.Product.Price * .9d;
        //                     else
        //                         thisAmount += line.Quantity * line.Product.Price;
        //                     break;
        //                 case Product.Prices.TwoThousand:
        //                     if (line.Quantity >= 3)
        //                         thisAmount += line.Quantity * line.Product.Price * .8d;
        //                     else
        //                         thisAmount += line.Quantity * line.Product.Price;
        //                     break;
        //             }

        //             result.Append(
        //                 $"<li>{line.Quantity} x {line.Product.ProductType} {line.Product.ProductName} = {thisAmount:C}</li>");
        //             totalAmount += thisAmount;
        //         }

        //         result.Append("</ul>");
        //     }

        //     result.Append($"<h3>Subtotal: {totalAmount:C}</h3>");
        //     var totalTax = totalAmount * Product.Prices.TaxRate;
        //     result.Append($"<h3>MVA: {totalTax:C}</h3>");
        //     result.Append($"<h2>Total: {totalAmount + totalTax:C}</h2>");
        //     result.Append("</body></html>");
        //     return result.ToString();
        // }

        /// <summary>
        /// This method is responsible to apply discount
        /// for instance, If a product quantity is equal or more than 10 make 50 % discount
        /// </summary>
        private void DoNumeralDiscount()
        {
            _orderLines.Where(x => x.Quantity >= _numeralDiscountNumber).ToList()
                .ForEach(x => x.DiscountedPrice = x.Product.Price * (0.01d * _numeralDiscountRate));
        }

        // private void CalculateTotalAmount()
        // {
        //     TotalAmount = _orderLines.Sum(x => x.DiscountedPrice);
        // }

        /// <summary>
        /// This method is responsible to apply discount
        /// for instance, If a product quantity is equal or more than 10 make 50 % discount
        /// </summary>
        private void DoFlatDiscount()
        {
            TotalAmount -= _flatDiscount;
        }

        // private void CalculateTax()
        // {
        //     _tax = TotalAmount * _taxRate;
        // }
        /// <summary>
        /// This method apply both numerical and flat discount on an order.
        /// </summary>
        public void Calculate()
        {
            DoNumeralDiscount();
            //CalculateTotalAmount();
            DoFlatDiscount();
            //CalculateTax();
        }
        /// <summary>
        /// This method use a factory to generate a reciept
        /// The factory get the type of reciept and then initialize a reciept generator
        /// </summary>
        /// <param name="type">type of reciept generator class for example Json</param>
        /// <returns></returns>
        public string GenerateReceipt(Factory.RecieptType type)
        {
            var generator = new Factory().FactoryClass(type);
            return generator.Generate(this);
        }
    }

}