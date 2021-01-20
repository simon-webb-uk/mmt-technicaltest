using System.Collections.Generic;

namespace Mmt.TechnicalTest.Core.Orders.OrderLookup.Models
{
    public class OrderLookupOrder
    {
        public int orderNumber { get; set; }
        public string orderDate { get; set; }

        public string deliveryAddress { get; set; }

        public List<OrderLookupItem> orderItems { get; set; }

        public string deliveryExpected { get; set; }
    }
}
