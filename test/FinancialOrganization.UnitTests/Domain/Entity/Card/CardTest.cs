namespace FinancialOrganization.UnitTests.Domain.Entity.Card;

using Bogus.DataSets;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FluentAssertions;
using DomainEntity = API.Domain.Entity;

[Collection(nameof(CardTestFixture))]
public class CardTest
{

    private readonly CardTestFixture _cardTestFixture;

    public CardTest(CardTestFixture cardTestFixture)
    {
        _cardTestFixture = cardTestFixture;
    }

    [Fact(DisplayName = nameof(Instatiate))]
    [Trait("Domain", "Card")]
    public void Instatiate()
    {
        var validCard = _cardTestFixture.GetValidCard();
        var datetimeBefore = DateTime.UtcNow;

        var category = new DomainEntity.Card(validCard.Name);
        var datetimeAfter = DateTime.UtcNow;

        category.Name.Should().Be(validCard.Name);
        category.Id.Should().NotBe(default(Guid));
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        category.CreatedAt.Should().NotBeBefore(datetimeBefore);
        category.CreatedAt.Should().NotBeAfter(datetimeAfter);
    }

    [Theory(DisplayName = nameof(ErroWhenNameIsEmpty))]
    [Trait("Domain", "Card")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void ErroWhenNameIsEmpty(string? name)
    {
        Action action =
            () => new DomainEntity.Card(name!);

        var exception = Assert.Throws<ErrorOnValidationException>(action);
        exception._errors.Should().ContainSingle().And.Contain(e => e.Equals("Name is required"));
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Card")]
    public void Update()
    {
        var card = _cardTestFixture.GetValidCard();

        var newCard = _cardTestFixture.GetValidCard();

        card.UpdateName(newCard.Name);

        card.Name.Should().Be(newCard.Name);
    }

    [Theory(DisplayName = nameof(UpdateErroWhenNameIsEmpty))]
    [Trait("Domain", "Card")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void UpdateErroWhenNameIsEmpty(string? name)
    {
        var card = _cardTestFixture.GetValidCard();
        Action action =
            () => card.UpdateName(name!);

        var exception = Assert.Throws<ErrorOnValidationException>(action);
        exception._errors.Should().ContainSingle().And.Contain(e => e.Equals("Name is required"));
    }
}
