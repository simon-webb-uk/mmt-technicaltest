using Mmt.TechnicalTest.Core.Customers;

namespace Mmt.TechnicalTest.Services.UnitTests.Fakes
{
    public static class FakeCustomers
    {
        public static Customer Default
        {
            get
            {
                return new Customer
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
            }
        }
    }
}
