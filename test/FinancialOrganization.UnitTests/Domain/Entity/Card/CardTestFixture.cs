using Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.UnitTests.Domain.Entity.Card;
public class CardTestFixture : BaseFixture
{
    public CardTestFixture() : base() { }

    public DomainEntity.Card GetValidCard()
        => new DomainEntity.Card(
            Faker.Person.FirstName
            );
}

[CollectionDefinition(nameof(CardTestFixture))]
public class CardTestFixtureCollection : ICollectionFixture<CardTestFixture> { }
