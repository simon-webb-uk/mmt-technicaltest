using Mmt.TechnicalTest.Core.Orders.OrderLookup;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Core.Customers
{
    public interface ICustomerService
    {
        public Task<IDataOperationResult<OrderLookupResponse>> GetRecentOrderAsync(string email, string customerId);
    }
}
