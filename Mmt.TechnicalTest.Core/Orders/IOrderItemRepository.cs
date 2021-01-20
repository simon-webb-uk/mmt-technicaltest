using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Core.Orders
{
    public interface IOrderItemRepository
    {
        public Task<IDataOperationResult<List<OrderItem>>> FindAsync(int orderId);
    }
}
