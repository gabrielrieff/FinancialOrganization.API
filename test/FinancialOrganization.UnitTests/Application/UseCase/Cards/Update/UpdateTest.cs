using FinancialOrganization.API.Application.UseCase.Cards.Delete;
using FinancialOrganization.API.Application.UseCase.Cards.Update;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FinancialOrganization.UnitTests.Application.UseCase.Cards.Delete;
using FluentAssertions;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Update;

[Collection(nameof(UpdateTestFixture))]
public class UpdateTest
{
    private readonly UpdateTestFixture _updateTestFixture;

    public UpdateTest(UpdateTestFixture updateTestFixture)
    {
        _updateTestFixture = updateTestFixture;
    }

    [Fact(DisplayName = nameof(UpdateCard))]
    [Trait("Application", "UpdateTest - Use Cases")]
    public async void UpdateCard()
    {
        var repositoryMock = _updateTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _updateTestFixture.GetUnitOfWorkMock();
        var card = _updateTestFixture.GetCard();

        repositoryMock.Setup(repository => repository.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(card);

        var useCase = new UpdateCardUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var request = _updateTestFixture.GetRequest();

        var act = async () => await useCase.Execute(request, card.Id, CancellationToken.None);
        await act.Should().NotThrowAsync();

        card.Should().NotBeNull();
        card.Name.Should().Be(request.Name);
        card.Id.Should().NotBeEmpty();
    }

    [Fact(DisplayName = nameof(ErrorCardNotFound))]
    [Trait("Application", "UpdateTest - Use Cases")]
    public async Task ErrorCardNotFound()
    {
        var repositoryMock = _updateTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _updateTestFixture.GetUnitOfWorkMock();
        var card = _updateTestFixture.GetCard();

        var useCase = new UpdateCardUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);


        var request = _updateTestFixture.GetRequest();

        var act = async () => await useCase.Execute(request, Guid.NewGuid(), CancellationToken.None);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Not found Card"));
    }
}
