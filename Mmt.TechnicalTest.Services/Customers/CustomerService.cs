using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Orders;
using Mmt.TechnicalTest.Core.Orders.OrderLookup;
using Mmt.TechnicalTest.Core.Orders.OrderLookup.Builders;
using Mmt.TechnicalTest.Core.Patterns.Operations;
using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Services.Customers
{
    /// <summary>
    /// Customer Service
    /// </summary>
    /// <seealso cref="Mmt.TechnicalTest.Core.Customers.ICustomerService" />
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        // Composition used here to aid refactoring in case alternative builder need to be injected.
        private readonly OrderLookupResponseBuilder _orderLookupResponseBuilder = new OrderLookupResponseBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository.</param>
        /// <param name="orderRepository">The order repository.</param>
        /// <param name="orderItemRepository">The order item repository.</param>
        /// <exception cref="System.ArgumentNullException">
        /// customerRepository
        /// or
        /// orderRepository
        /// or
        /// orderItemRepository
        /// </exception>
        public CustomerService(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
        }

        /// <inheritdoc />
        public async Task<IDataOperationResult<OrderLookupResponse>> GetRecentOrderAsync(string email, string customerId)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException($"'{nameof(customerId)}' cannot be null or whitespace", nameof(customerId));
            }

            var customerLookup = await _customerRepository.GetAsync(email);
            if (customerLookup.Result != OperationStatus.Ok)
            {
                return OperationResult.WithData<OrderLookupResponse>
                    .NoRecords;
            }

            var customer = customerLookup.Data;

            // Supplied Customer ID must match the record in the datastore. Otherwise not found.
            if (customer.CustomerId != customerId)
            {
                return OperationResult.WithData<OrderLookupResponse>
                    .NoRecords;
            }

            Order order = null;
            List<OrderItem> items = null;

            var orderLookup = await _orderRepository.GetMostRecent(customer.CustomerId);
            if (orderLookup.Result == OperationStatus.Ok)
            {
                order = orderLookup.Data;

                var orderItemLookup = await _orderItemRepository.FindAsync(order.OrderId);
                if (orderItemLookup.Result == OperationStatus.Ok)
                {
                    items = orderItemLookup.Data;
                }
            }

            var output = _orderLookupResponseBuilder.Build(
                customer, order, items);

            return OperationResult.WithData<OrderLookupResponse>
                .Ok(output);
        }
    }
}
