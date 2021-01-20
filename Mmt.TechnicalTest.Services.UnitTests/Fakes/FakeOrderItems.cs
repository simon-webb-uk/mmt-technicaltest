using Mmt.TechnicalTest.Core.Orders;
using System.Collections.Generic;

namespace Mmt.TechnicalTest.Services.UnitTests.Fakes
{
    public static class FakeOrderItems
    {
        public static List<OrderItem> Default
        {
            get
            {
                return new List<OrderItem> { 
                
                    new OrderItem
                    {
                        Price = 1,
                        ProductName = "Test Product Name",
                        Quantity = 1
                    }
                };
            }
        }
    }
}
