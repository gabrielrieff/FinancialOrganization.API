using Codeflix.Catalog.UnitTests.Common;
using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Register;
public class RegisterTestFixture : BaseFixture
{
    public RegisterCardRequest GetRequest() => new RegisterCardRequest { Name = Faker.Commerce.ProductName()};
    public RegisterCardRequest GetRequestInvalid(string? name) => new RegisterCardRequest { Name = name!};
    public Mock<ICardRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

}

[CollectionDefinition(nameof(RegisterTestFixture))]
public class RegisterTestFixtureCollection : ICollectionFixture<RegisterTestFixture> { }
