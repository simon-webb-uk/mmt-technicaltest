using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Core.Orders
{
    public interface IOrderRepository
    {
        public Task<IDataOperationResult<Order>> GetMostRecent(string customerId);
    }
}
