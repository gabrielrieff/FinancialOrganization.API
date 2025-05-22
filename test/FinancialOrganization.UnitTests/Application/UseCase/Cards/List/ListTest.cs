using FinancialOrganization.API.Application.UseCase.Cards.GetAll;
using FinancialOrganization.API.Communication.Response;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.Enums;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.GetAll;
[Collection(nameof(ListTestFixture))]
public class ListTest
{
    private readonly ListTestFixture _getAllTestFixture;

    public ListTest(ListTestFixture getAllTestFixture)
    {
        _getAllTestFixture = getAllTestFixture;
    }

    [Fact(DisplayName = nameof(GetAll))]
    [Trait("Application", "GetAllTest - Use Cases")]
    public async void GetAll()
    {
        var repositoryMock = _getAllTestFixture.GetRepositoryMock();
        var cardsExemploList = _getAllTestFixture.GetCollection(count: 20);

        var request = new SearchListRequest(
                page: 2,
                perPage: 10,
                search: "card-exemplo",
                sort: "name",
                dir: SearchOrder.Asc
            );

        var outputRepositorySearch = new SearchOutput<Card>(
            currentPage: request.Page,
            perPage: request.PerPage,
            total: 70,
            items: cardsExemploList
        );

        repositoryMock.Setup(repository => repository.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == request.Page
                && searchInput.PerPage == request.PerPage
                && searchInput.Search == request.Search
                && searchInput.OrderBy == request.Sort
                && searchInput.Order == request.Dir
            ),
            It.IsAny<CancellationToken>())).ReturnsAsync(outputRepositorySearch);

        var useCase = new ListUseCase(repositoryMock.Object);

        var result = await useCase.Execute(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(outputRepositorySearch.CurrentPage);
        result.PerPage.Should().Be(outputRepositorySearch.PerPage);
        result.Total.Should().Be(outputRepositorySearch.Total);
        result.Items.Should().NotBeNullOrEmpty().And.HaveCount(cardsExemploList.Count);

        result.Items.Should().NotBeNullOrEmpty().And.AllSatisfy(card =>
        {
            card.Id.Should().NotBe(default(Guid));
            card.Name.Should().NotBeNullOrEmpty();
        });
    }
}
