using Mmt.TechnicalTest.Core.Orders.OrderLookup.Models;

namespace Mmt.TechnicalTest.Core.Orders.OrderLookup
{
    public class OrderLookupResponse
    {
        public OrderLookupCustomer customer { get; set; }
        public OrderLookupOrder order { get; set; }
    }
}
