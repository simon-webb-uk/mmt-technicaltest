using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Core.Customers
{
    public interface ICustomerRepository
    {
        public Task<IDataOperationResult<Customer>> GetAsync(string email);
    }
}
