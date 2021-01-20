using NUnit.Framework;
using Flurl.Http.Testing;
using Mmt.TechnicalTest.Core.Customers;
using Mmt.TechnicalTest.Repositories.Customers;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Net.Http;
using Mmt.TechnicalTest.Core.Patterns.Operations.Enum;
using System.Web;

namespace Mmt.TechnicalTest.Repositories.UnitTests
{
    // TODO - Contains example tests, better coverage would typically be implemented.

    public class CustomerApiRepositoryTests
    {
        private readonly Customer _mockData = new Customer
        {
            Email = "bob@mmtdigital.co.uk",
            CustomerId = "R223232",
            Website = true,
            FirstName = "Bob",
            LastName = "Testperson",
            LastLoggedIn = "03-May-2021 09:15",
            HouseNumber = "1a",
            Street = "Uppingham Gate",
            Town = "Uppingham",
            Postcode = "LE15 9NY",
            PreferredLanguage = "en-gb"
        };

        private readonly static string _baseUrl = "http://test.123.com/api";
        private readonly static string _apiKey = "xxxAPIKEYxxx";

        private readonly IOptions<CustomerApiConfig> _options = Options.Create(new CustomerApiConfig { ApiKey = _apiKey, BaseUri = _baseUrl });

        [Test]
        public async Task ShouldCallTheEndpointCorrectly()
        {
            using var httpTest = new HttpTest();

            // arrange
            var email = "some@email.com";
            httpTest.RespondWithJson(_mockData, 200);
            var sut = new CustomerApiRepository(_options);

            // act
            await sut.GetAsync(email);

            // assert
            httpTest.ShouldHaveCalled($"http://test.123.com/api/GetUserDetails?email={HttpUtility.UrlEncode(email)}&code={_apiKey}")
                .WithVerb(HttpMethod.Get)
                .WithQueryParams(new { email });
        }

        [Test(Description = "Should Return Correct Operation Result - to indicate the Customer was found")]
        public async Task ShouldReturnCorrectOperationResult()
        {
            using var httpTest = new HttpTest();

            // arrange
            var email = "some@email.com";
            httpTest.RespondWithJson(_mockData, 200);
            var sut = new CustomerApiRepository(_options);

            // act
            var result = await sut.GetAsync(email);

            // assert
            Assert.AreEqual(result.Result, OperationStatus.Ok);
        }

        [Test(Description = "Should return the correct customer ID")]
        public async Task ShouldReturnCorrectCustomerId()
        {
            using var httpTest = new HttpTest();

            // arrange
            var email = "some@email.com";
            httpTest.RespondWithJson(_mockData, 200);
            var sut = new CustomerApiRepository(_options);

            // act
            var result = await sut.GetAsync(email);

            // assert
            Assert.AreEqual(result.Data.CustomerId, _mockData.CustomerId);
        }
    }
}