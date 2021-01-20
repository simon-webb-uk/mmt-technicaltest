using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Formatters;
using Mmt.TechnicalTest.Core.Orders.OrderLookup.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mmt.TechnicalTest.Core.Orders.OrderLookup.Builders
{
    public class OrderLookupResponseBuilder
    {
        public OrderLookupResponse Build(Customer customer, Order order, List<OrderItem> items)
        {
            return new OrderLookupResponse
            {
                customer = new OrderLookupCustomer
                {
                    firstName = customer.FirstName,
                    lastName = customer.LastName
                },
                order = BuildOrderLookupOrder(customer, order, items)
            };
        }

        private OrderLookupOrder BuildOrderLookupOrder(Customer customer, Order order, List<OrderItem> items)
        {
            if (order is null)
            {
                return null;
            }

            var orderItems = items.Select(a => new OrderLookupItem()
            {
                priceEach = a.Price,
                product = order.ContainsGift ? "Gift" : a.ProductName,
                quantity = a.Quantity

            }).ToList();

            return new OrderLookupOrder
            {
                deliveryAddress = BuildAddress(customer),
                deliveryExpected = DateTimeFormatter.ToShortDateString(order.DeliveryExpected),
                orderDate = DateTimeFormatter.ToShortDateString(order.OrderDate),
                orderItems = orderItems,
                orderNumber = order.OrderId
            };
        }

        private string BuildAddress(Customer customer)
        {
            var valuesToAppend = new List<string>();

            valuesToAppend.Add($"{customer.HouseNumber.ToUpper()} {customer.Street}");

            if (!string.IsNullOrWhiteSpace(customer.Town))
            {
                valuesToAppend.Add(customer.Town);
            }

            if (!string.IsNullOrWhiteSpace(customer.Postcode))
            {
                valuesToAppend.Add(customer.Postcode);
            }

            return string.Join(", ", valuesToAppend);
        }
    }
}
