using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using OrderService.Entities;
using OrderService.Services;
// using OrderService.Entities;
// using OrderService.Services;

namespace OrderService.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private static readonly Product MotorSuper = new Product("Car Insurance", "Super", Product.Prices.TwoThousand);
        private static readonly Product MotorBasic = new Product("Car Insurance", "Basic", Product.Prices.OneThousand);
        private static readonly Product Disability = new Product("Disability Insurance", "Basic", Product.Prices.OneThousand);


    
        [Test]
        public void can_generate_json_for_complex_basic()
        {
            var order = new Order(company:"Test Company",taxRate:0.25d,flatDiscount: 100,numeralDiscountRate: 50, 
            numeralDiscountNumber: 10);
            var orderlines=new List<OrderLine>();
            orderlines.Add(new OrderLine(MotorBasic, 1));
            orderlines.Add(new OrderLine(Disability, 10));
            order.AddLines(orderlines);
            var actual = order.GenerateReceipt(Factory.RecieptType.Json);

            var expected =
                "{\"_orderLines\":[{\"DiscountedPrice\":1000.0,\"Product\":{\"ProductType\":\"Car Insurance\",\"ProductName\":\"Basic\",\"Price\":1000},\"Quantity\":1},{\"DiscountedPrice\":500.0,\"Product\":{\"ProductType\":\"Disability Insurance\",\"ProductName\":\"Basic\",\"Price\":1000},\"Quantity\":10}],\"TotalAmount\":1400.0,\"TotalWithTax\":1750.0,\"Tax\":350.0,\"Company\":\"Test Company\"}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void can_generate_html_for_complex_order()
        {
            var order = new Order(company:"Test Company",taxRate:0.25d,flatDiscount: 100,numeralDiscountRate: 50, 
                                    numeralDiscountNumber: 10);
            var orderlines=new List<OrderLine>();
            orderlines.Add(new OrderLine(MotorBasic, 1));
            orderlines.Add(new OrderLine(Disability, 10));
            order.AddLines(orderlines);
            var actual = order.GenerateReceipt(Factory.RecieptType.Html);

            var expected =$"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Basic = $1,000.00</li><li>10 x Disability Insurance Basic = $500.00</li></ul><h3>Subtotal: $1,400.00</h3><h3>MVA: $350.00</h3><h2>Total: $1,750.00</h2></body></html>";

            Assert.AreEqual(expected, actual);
        }

        // [Test]
        // public void can_generate_html_receipt_for_motor_super()
        // {
        //     var order = new Order("Test Company",0.25d,100);
        //     order.AddLine(new OrderLine(MotorSuper, 1));
        //     var actual = order.GenerateHtmlReceipt();

        //     var expected =
        //         $"<html><body><h1>Order receipt for 'Test Company'</h1><ul><li>1 x Car Insurance Super = kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</li></ul><h3>Subtotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00</h3><h3>MVA: kr 500,00</h3><h2>Total: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}500,00</h2></body></html>";

        //     Assert.AreEqual(expected, actual);
        // }

        // [Test]
        // public void can_generate_receipt_for_motor_basic()
        // {
        //     var order = new Order("Test Company");
        //     order.AddLine(new OrderLine(MotorBasic, 1));
        //     var actual = order.GenerateReceipt();
        //     var expected =
        //         $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Basic = kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nSubtotal: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nMVA: kr 250,00\r\nTotal: kr 1{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}250,00";

        //     Assert.AreEqual(expected, actual);
        // }

        // [Test]
        // public void can_generate_receipt_for_motor_super()
        // {
        //     var order = new Order("Test Company");
        //     order.AddLine(new OrderLine(MotorSuper, 1));
        //     var actual = order.GenerateReceipt();
        //     var expected =
        //         $"Order receipt for 'Test Company'\r\n\t1 x Car Insurance Super = kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nSubtotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}000,00\r\nMVA: kr 500,00\r\nTotal: kr 2{NumberFormatInfo.CurrentInfo.NumberGroupSeparator}500,00";

        //     Assert.AreEqual(expected, actual);
        // }
    }
}