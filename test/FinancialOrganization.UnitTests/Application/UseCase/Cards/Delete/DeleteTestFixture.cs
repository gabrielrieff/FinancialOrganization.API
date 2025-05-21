using Codeflix.Catalog.UnitTests.Common;
using FinancialOrganization.API.Communication.Request.Cards;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Delete;
public class DeleteTestFixture : BaseFixture
{
    public Card GetCard() => new Card(Faker.Person.FirstName);
    public UpdateCardRequest GetRequest() => new UpdateCardRequest { Name = Faker.Commerce.ProductName() };
    public Mock<ICardRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
}

[CollectionDefinition(nameof(DeleteTestFixture))]
public class DeleteTestFixtureCollection : ICollectionFixture<DeleteTestFixture> { }
