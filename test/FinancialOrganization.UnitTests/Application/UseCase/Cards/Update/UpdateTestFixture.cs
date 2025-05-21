using Codeflix.Catalog.UnitTests.Common;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories;
using Moq;
using FinancialOrganization.API.Communication.Request.Cards;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Update;
public class UpdateTestFixture : BaseFixture
{
    public Card GetCard() => new Card(Faker.Person.FirstName);
    public UpdateCardRequest GetRequest() => new UpdateCardRequest { Name = Faker.Commerce.ProductName() };
    public UpdateCardRequest GetRequestInvalid(string? name) => new UpdateCardRequest { Name = name! };
    public Mock<ICardRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
}

[CollectionDefinition(nameof(UpdateTestFixture))]
public class UpdateTestFixtureCollection : ICollectionFixture<UpdateTestFixture> { }