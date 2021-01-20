using Microsoft.Extensions.Options;
using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Core.Patterns.Operations.Interfaces;
using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Mmt.TechnicalTest.Core.Patterns.Operations;
using System.Net;

namespace Mmt.TechnicalTest.Repositories.Customers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Mmt.TechnicalTest.Core.Customers.ICustomerRepository" />
    public class CustomerApiRepository : ICustomerRepository
    {
        private readonly CustomerApiConfig _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerApiRepository"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public CustomerApiRepository(IOptions<CustomerApiConfig> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.Value;
        }

        /// <inheritdoc/>
        public async Task<IDataOperationResult<Customer>> GetAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace", nameof(email));
            }

            try
            {
                var callResult = await _options.BaseUri
                    .AppendPathSegment("GetUserDetails")
                    .SetQueryParams(new { email, code = _options.ApiKey })
                    .GetJsonAsync<Customer>();


                return OperationResult.WithData<Customer>
                    .Ok(callResult);

            }
            catch (FlurlHttpException ex) when (
                ex.StatusCode == (int)HttpStatusCode.BadRequest ||
                ex.StatusCode == (int)HttpStatusCode.Unauthorized
               )
            {
                return OperationResult.WithData<Customer>
                    .Failed;
            }
            catch
            {
                return OperationResult.WithData<Customer>
                    .NoRecords;
            }
        }
    }
}
