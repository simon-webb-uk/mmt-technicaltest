using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Customers.RecentOrders;
using Mmt.TechnicalTest.Core.Orders.OrderLookup;
using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using System;
using System.Threading.Tasks;

namespace MMT.TechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : ControllerBase
    {
        /// <summary>
        /// The Customer Service
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderController"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        /// <exception cref="System.ArgumentNullException">customerService</exception>
        public CustomerOrderController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// Gets the most recent order details for the specified customer.
        /// </summary>
        /// <param name="user">The user.</param>    
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpPost("recent/")]
        [ProducesResponseType(typeof(OrderLookupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderLookupResponse>> GetRecentAsync(RecentOrdersRequest requestBody)
        {
            var get = await _customerService.GetRecentOrderAsync(requestBody.Email, requestBody.CustomerId);

            if (get.Result == OperationStatus.NoRecords)
            {
                // TODO: Implement a Custom Problem Details Factory
                // see https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0
                return NotFound();
            }

            return Ok(get.Data);
        }
    }
}
