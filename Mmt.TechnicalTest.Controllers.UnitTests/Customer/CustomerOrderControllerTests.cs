using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Customers.RecentOrders;
using Mmt.TechnicalTest.Core.Orders.OrderLookup;
using Mmt.TechnicalTest.Core.Patterns.Operations;
using MMT.TechnicalTest.Controllers;
using Moq;
using NUnit.Framework;

namespace Mmt.TechnicalTest.Controllers.UnitTests.Customer
{
    public class CustomerOrderControllerTests
    {
        [Test(Description = "If the service returns OK, the controller should return a 200")]
        public async Task GetRecentOrderAsync_OK()
        {
            // Arrange
            var mockSetup = new Mock<ICustomerService>();
            mockSetup
                .Setup(repo => repo.GetRecentOrderAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(
                    OperationResult.WithData<OrderLookupResponse>.Ok(new OrderLookupResponse { })
                );
            var mock = mockSetup.Object;


            var controller = new CustomerOrderController(mock);

            // Act
            var action = await controller.GetRecentAsync(new RecentOrdersRequest
            {
                CustomerId = "x",
                Email = "x"
            });

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(action.Result);
        }

        [Test(Description = "If the service returns Not Found, the controller should return a 404")]
        public async Task GetRecentOrderAsync_NotFound()
        {
            // Arrange
            var mockSetup = new Mock<ICustomerService>();
            mockSetup
                .Setup(repo => repo.GetRecentOrderAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(
                    OperationResult.WithData<OrderLookupResponse>.NoRecords
                );
            var mock = mockSetup.Object;


            var controller = new CustomerOrderController(mock);

            // Act
            var action = await controller.GetRecentAsync(new RecentOrdersRequest
            {
                CustomerId = "x",
                Email = "x"
            });

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(action.Result);
        }
    }
}
