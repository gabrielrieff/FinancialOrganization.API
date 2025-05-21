using FinancialOrganization.API.Application.UseCase.Cards.Delete;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FluentAssertions;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Delete;

[Collection(nameof(DeleteTestFixture))]
public class DeleteTest
{
    private readonly DeleteTestFixture _deleteTestFixture;

    public DeleteTest(DeleteTestFixture deleteTestFixture)
    {
        _deleteTestFixture = deleteTestFixture;
    }

    [Fact(DisplayName = nameof(Delete))]
    [Trait("Application", "DeleteTest - Use Cases")]
    public async void Delete()
    {
        var repositoryMock = _deleteTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _deleteTestFixture.GetUnitOfWorkMock();
        var card = _deleteTestFixture.GetCard();

        repositoryMock.Setup(repository => repository.GetById(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(card);

        var useCase = new DeleteCardUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var act = async () => await useCase.Execute(card.Id, CancellationToken.None);
        await act.Should().NotThrowAsync();
    }

    [Fact(DisplayName = nameof(ErrorCardNotFound))]
    [Trait("Application", "DeleteTest - Use Cases")]
    public async Task ErrorCardNotFound()
    {
        var repositoryMock = _deleteTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _deleteTestFixture.GetUnitOfWorkMock();
        var card = _deleteTestFixture.GetCard();

        var useCase = new DeleteCardUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var act = async () => await useCase.Execute(Guid.NewGuid(), CancellationToken.None);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Not found Card"));
    }
}
