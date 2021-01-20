using Dapper;
using Microsoft.Extensions.Options;
using Mmt.TechnicalTest.Core.Orders;
using Mmt.TechnicalTest.Core.Patterns.Operations;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using Mmt.TechnicalTest.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mmt.TechnicalTest.Repositories.Orders
{
    /// <summary>
    /// Order Items Database Repository
    /// </summary>
    /// <seealso cref="Mmt.TechnicalTest.Core.Orders.IOrderItemRepository" />
    public class OrderItemDbRepository : IOrderItemRepository
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemDbRepository" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public OrderItemDbRepository(IOptions<OrderDbConfig> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _db = new SqlConnection(options.Value.ConnectionString);
        }

        /// <inheritdoc />
        public async Task<IDataOperationResult<List<OrderItem>>> FindAsync(int orderId)
        {
            if (orderId < 1)
            {
                throw new ArgumentOutOfRangeException($"'{nameof(orderId)}' cannot be less than 1");
            }

            var sql = "SELECT " +
                $" PRODUCTNAME," +
                $" QUANTITY, " +
                $" PRICE " +
                $" FROM ORDERITEMS INNER JOIN PRODUCTS ON ORDERITEMS.PRODUCTID = PRODUCTS.PRODUCTID " +
                $" WHERE ORDERID = @Id";

            var dbLookup = await _db.QueryAsync<OrderItem>(sql, new { id = orderId });

            if (!dbLookup.Any())
            {
                return OperationResult.WithData<List<OrderItem>>
                    .NoRecords;
            }

            return OperationResult.WithData<List<OrderItem>>
                .Ok(dbLookup.ToList());
        }
    }
}
