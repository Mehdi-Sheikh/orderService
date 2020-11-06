using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OrderService.Services;

namespace OrderService.Entities
{
    public class Order
    {
        public readonly List<OrderLine> _orderLines = new List<OrderLine>();
        public double TotalAmount
        {
            get => _orderLines.Sum(x => x.DiscountedPrice)-_flatDiscount; //calculate sum of orderlines when discount has been calculated
        }
        public double TotalWithTax{get=> Tax + TotalAmount;}
        public double Tax { get { return TotalAmount * _taxRate; }}//Calculate tax
        private double _taxRate;
        private int _numeralDiscountRate;// Discount percent which will be applied 
        private int _numeralDiscountNumber;//Threshold to have discount if an orderlines quantity is more than that
        private double _flatDiscount;//a flat discount which will be applied to each order
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
            _orderLines.AddRange(orderLines);
            DoNumeralDiscount();
        }

        
        /// <summary>
        /// This method is responsible to apply discount
        /// for instance, If a product quantity is equal or more than 10 make 50 % discount
        /// </summary>
        private void DoNumeralDiscount()
        {
            _orderLines.Where(x => x.Quantity >= _numeralDiscountNumber).ToList()
                .ForEach(x => x.DiscountedPrice = x.Product.Price * (0.01d * _numeralDiscountRate));
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