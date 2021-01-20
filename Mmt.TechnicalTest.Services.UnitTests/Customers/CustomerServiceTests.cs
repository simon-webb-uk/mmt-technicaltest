using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Orders;
using Mmt.TechnicalTest.Core.Patterns.Operations;
using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using Mmt.TechnicalTest.Services.Customers;
using Mmt.TechnicalTest.Services.UnitTests.Fakes;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Services.UnitTests.Customers
{
    // TODO - Contains example tests, better coverage would typically be implemented.

    public class CustomerServiceTests
    {
        private IOrderRepository _mockOrderRepo;
        private IOrderItemRepository _mockOrderItemRepo;
        private ICustomerRepository _mockCustomerRepo;

        [SetUp]
        public void Setup()
        {
            var mockOrderRepoSetup = new Mock<IOrderRepository>();
            mockOrderRepoSetup
                .Setup(repo => repo.GetMostRecent(
                    It.IsAny<string>()))
                .ReturnsAsync(
                    OperationResult.WithData<Order>.Ok(FakeOrders.Default)
                );
            _mockOrderRepo = mockOrderRepoSetup.Object;

            var mockOrderItemRepoSetup = new Mock<IOrderItemRepository>();
            mockOrderItemRepoSetup
                .Setup(repo => repo.FindAsync(
                    It.IsAny<int>()))
                .ReturnsAsync(
                    OperationResult.WithData<List<OrderItem>>.Ok(FakeOrderItems.Default)
                );
            _mockOrderItemRepo = mockOrderItemRepoSetup.Object;

            var mockCustomerRepoSetup = new Mock<ICustomerRepository>();
            mockCustomerRepoSetup
                .Setup(repo => repo.GetAsync(
                    It.IsAny<string>()))
                .ReturnsAsync(
                    OperationResult.WithData<Customer>.Ok(FakeCustomers.Default)
                );
            _mockCustomerRepo = mockCustomerRepoSetup.Object;
        }

        [Test(Description = "On Customer And Order Found - Order Id is mapped to order.orderNumber")]
        public async Task OnCustomerAndOrderFound_OrderIdIsMappedToOrderOrderNumber()
        {
            // arrange
            var sut = new CustomerService(
                _mockCustomerRepo,
                _mockOrderRepo,
                _mockOrderItemRepo);

            // act
            var testResult = await sut.GetRecentOrderAsync(FakeCustomers.Default.Email, FakeCustomers.Default.CustomerId);

            // assert
            Assert.AreEqual(testResult.Data.order.orderNumber, FakeOrders.Default.OrderId);
        }

        [Test(Description = "If the Customer ID supplied does not match the datastore, return No Records")]
        public async Task ReturnNoRecordsIfCustomerIdSuppliedDoesNotMatchDatastore()
        {
            // arrange
            var sut = new CustomerService(
                _mockCustomerRepo,
                _mockOrderRepo,
                _mockOrderItemRepo);

            // act
            var testResult = await sut.GetRecentOrderAsync(FakeCustomers.Default.Email, "invalid");

            // assert
            Assert.AreEqual(testResult.Result, OperationStatus.NoRecords);
        }

        [Test(Description = "On Customer And Order Found - If the order contains a gift, the item.product should be 'Gift'")]
        public async Task OnCustomerAndOrderFound_ContainsGift()
        {
            // arrange
            var giftOrder = FakeOrders.Default;
            giftOrder.ContainsGift = true;

            var mockOrderRepoSetup = new Mock<IOrderRepository>();
            mockOrderRepoSetup
                .Setup(repo => repo.GetMostRecent(
                    It.IsAny<string>()))
                .ReturnsAsync(
                    OperationResult.WithData<Order>.Ok(giftOrder)
                );
            var mockGiftOrderRepo = mockOrderRepoSetup.Object;

            var sut = new CustomerService(
                _mockCustomerRepo,
                mockGiftOrderRepo,
                _mockOrderItemRepo);

            // act
            var testResult = await sut.GetRecentOrderAsync(FakeCustomers.Default.Email, FakeCustomers.Default.CustomerId);

            // assert
            var testOrderItem = testResult.Data.order.orderItems.FirstOrDefault();

            Assert.AreEqual(testOrderItem.product, "Gift");
        }
    }
}