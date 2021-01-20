using Microsoft.Extensions.Options;
using Mmt.TechnicalTest.Core.Orders;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using Mmt.TechnicalTest.Repositories.Customers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Mmt.TechnicalTest.Core.Patterns.Operations;

namespace Mmt.TechnicalTest.Repositories.Orders
{
    /// <summary>
    /// Orders Database Repository
    /// </summary>
    /// <seealso cref="Mmt.TechnicalTest.Core.Orders.IOrderRepository" />
    public class OrderDbRepository : IOrderRepository
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDbRepository"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public OrderDbRepository(IOptions<OrderDbConfig> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _db = new SqlConnection(options.Value.ConnectionString);
        }

        /// <inheritdoc />
        public async Task<IDataOperationResult<Order>> GetMostRecent(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException($"'{nameof(customerId)}' cannot be null or whitespace", nameof(customerId));
            }

            var sql = "SELECT TOP 1 * " +
                " FROM ORDERS " +
                " WHERE CUSTOMERID = @customerId " +
                " ORDER BY ORDERDATE DESC";
            var dbLookup = await _db.QuerySingleOrDefaultAsync<Order>(sql, new { customerId });

            if (dbLookup == null)
            {
                return OperationResult.WithData<Order>
                    .NoRecords;
            }

            return OperationResult.WithData<Order>
                .Ok(dbLookup);
        }
    }
}
