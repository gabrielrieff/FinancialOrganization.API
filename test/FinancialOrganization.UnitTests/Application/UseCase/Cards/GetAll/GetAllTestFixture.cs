using Codeflix.Catalog.UnitTests.Common;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Repositories.Cards;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.GetAll;
public class GetAllTestFixture : BaseFixture
{
    public Card GetCard() => new Card(Faker.Person.FirstName);
    public IList<Card> GetCollection(uint count = 2)
    {
        var list = new List<Card>();

        if (count == 0)
            count = 1;

        for (int i = 0; i < count; i++)
        {
            var card = GetCard();

            list.Add(card);
        }

        return list;
    }

    public Mock<ICardRepository> GetRepositoryMock() => new();
}
[CollectionDefinition(nameof(GetAllTestFixture))]
public class GetAllTestFixtureCollection : ICollectionFixture<GetAllTestFixture> { }
