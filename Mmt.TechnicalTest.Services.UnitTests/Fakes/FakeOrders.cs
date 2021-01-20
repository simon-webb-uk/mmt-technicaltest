using Mmt.TechnicalTest.Core.Orders;
using System;

namespace Mmt.TechnicalTest.Services.UnitTests.Fakes
{
    public static class FakeOrders
    {
        public static Order Default
        {
            get
            {
                return new Order
                {
                    ContainsGift = false,
                    DeliveryExpected = DateTime.Now,
                    OrderDate = DateTime.Now.AddDays(-1),
                    OrderId = 1               
                };
            }
        }
    }
}
