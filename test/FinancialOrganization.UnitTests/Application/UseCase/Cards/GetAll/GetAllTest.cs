using FinancialOrganization.API.Application.UseCase.Cards.GetAll;
using FluentAssertions;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.GetAll;
[Collection(nameof(GetAllTestFixture))]
public class GetAllTest
{
    private readonly GetAllTestFixture _getAllTestFixture;

    public GetAllTest(GetAllTestFixture getAllTestFixture)
    {
        _getAllTestFixture = getAllTestFixture;
    }

    [Fact(DisplayName = nameof(GetAll))]
    [Trait("Application", "GetAllTest - Use Cases")]
    public async void GetAll()
    {
        var repositoryMock = _getAllTestFixture.GetRepositoryMock();
        var cards = _getAllTestFixture.GetCollection();

        repositoryMock.Setup(repository => repository.GetAll(
            It.IsAny<Guid>(), 
            It.IsAny<CancellationToken>())).ReturnsAsync(cards);

        var useCase = new GetAllUseCase(repositoryMock.Object);

        var result = await useCase.Execute(CancellationToken.None);

        result.Should().NotBeNull();

        result.Cards.Should().NotBeNullOrEmpty().And.AllSatisfy(card =>
        {
            card.Id.Should().NotBe(default(Guid));
            card.Name.Should().NotBeNullOrEmpty();
        });
    }
}
