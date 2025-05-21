using FinancialOrganization.API.Application.UseCase.Cards.Register;
using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Exception.ExceptionsBase;
using FluentAssertions;
using Moq;

namespace FinancialOrganization.UnitTests.Application.UseCase.Cards.Register;

[Collection(nameof(RegisterTestFixture))]
public class RegisterTest
{
    private readonly RegisterTestFixture _registerTestFixture;

    public RegisterTest(RegisterTestFixture registerTestFixture)
    {
        _registerTestFixture = registerTestFixture;
    }

    [Fact(DisplayName = nameof(RegisterCard))]
    [Trait("Application", "RegisterTest - Use Cases")]
    public async void RegisterCard()
    {
        var repositoryMock = _registerTestFixture.GetRepositoryMock();
        var unitOfWorkMock = _registerTestFixture.GetUnitOfWorkMock();

        var useCase = new RegisterCardUseCase(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var request = _registerTestFixture.GetRequest();

        var output = await useCase.Execute(request, CancellationToken.None);

        repositoryMock.Verify(repository => repository.Register(
            It.IsAny<Card>(), 
            It.IsAny<CancellationToken>()));
        unitOfWorkMock.Verify(unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()));

        output.Should().NotBeNull();
        output.Name.Should().Be(request.Name);
        output.Id.Should().NotBeEmpty();
    }
    
    [Theory(DisplayName = nameof(ThrowWhenCantInstantiate))]
    [Trait("Application", "RegisterTest - Use Cases")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public async Task ThrowWhenCantInstantiate(string? name)
    {
        var useCase = new RegisterCardUseCase(
            _registerTestFixture.GetRepositoryMock().Object,
            _registerTestFixture.GetUnitOfWorkMock().Object);

        var request = _registerTestFixture.GetRequestInvalid(name);

        var action = async () => await useCase.Execute(request, CancellationToken.None);

        var result = await action.Should().ThrowAsync<ErrorOnValidationException>();
            result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains("Name is required"));

    }
}
