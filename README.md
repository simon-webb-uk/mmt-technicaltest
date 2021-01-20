## About

Technical test as requested by MMT Digital - [https://www.mmtdigital.co.uk/](https://www.mmtdigital.co.uk/)


## Setup
### MMT.TechnicalTest\appsettings.json
Before running add the following configuration entries as supplied in the test documentation
- CustomerApi.ApiKey for https://customer-details.azurewebsites.net
- OrderDb.ConnectionString

## Improvements

### Specification
- Ideally should be analysed and converted into a User Story with associated background information and Acceptence Criteria to avoid any ambiguity and hidden or implied requirements. 
- This would greatly improve the quality of the solution and increase the chances of getting it right first time. Acceptance Criteria enables developers to ensure they include tests for business logic and enable testers to begin planning before the coding is complete.
- e.g. Analysing the example Customer data and the example endpoint output seems to indicate when houseNumber and street are concatenated for transformation to order.deliveryAddress the house number should be converted to uppercase. This may be a business requirement but it's not clear, analysis would raise this to ensure if it should be implmented or not. In this instance I have assumed that it is a requirement but typically this is something I would raise with the relevant stakeholder.

### Code
- Testing, given the time allowed I have included example tests only. Typically, these would provide much greater coverage of requirements and to exercise the components before integrating them as well as to ensure greater confidence of any future changes.
- Authentication and Authorisation, typically an API of this nature would need to be secure as it contains highly sensitive customer information.
- Better Exception Handling, e.g. the repositories are currently somewhat brittle and need better fault tolerance/handling, e.g. when a database is unavailable.
- Application Health, add Application Insights to aid debugging and fault monitoring in production.
- Configuration, consider holding app settings in a key vault

## Contact

Simon Webb - sywebb@hotmail.com