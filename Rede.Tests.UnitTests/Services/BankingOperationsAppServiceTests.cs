using AutoMapper;
using Moq;
using Rede.Domain.Interfaces;
using Rede.Service.Services;
using Rede.Service.ViewModels;
using Xunit;

namespace Rede.Tests.UnitTests.Services;

public class BankingOperationsAppServiceTests
{
    [Fact]
    public void GetById()
    {
        // Arrange
        var bankingOperations = new Domain.Models.BankingOperations(new Guid(), "000002", "00", 100, "Deposit");

        var bankingOperationsRepositoryMock = new Mock<IBankingOperationsRepository>();
        
        bankingOperationsRepositoryMock.Setup(x => x.GetById(bankingOperations.Id))
            .Returns(bankingOperations);
        
        var mapperMock = new Mock<IMapper>();
        
        mapperMock.Setup(x => x.Map<BankingOperationsViewModel>(bankingOperations)).Returns(new BankingOperationsViewModel
        {
            Id = bankingOperations.Id,
            OriginAccount = bankingOperations.OriginAccount,
            DestinationAccount = bankingOperations.DestinatioAccount,
            Amount = bankingOperations.Amount,
            Operation = bankingOperations.Operation
        });

        // Act
        var sut = new BankingOperationsAppService(mapperMock.Object, bankingOperationsRepositoryMock.Object, null!);
        var result = sut.GetById(bankingOperations.Id);

        // Assert
        Assert.Equal(result.Id, bankingOperations.Id);
        Assert.Equal(result.OriginAccount, bankingOperations.OriginAccount);
        Assert.Equal(result.DestinationAccount, bankingOperations.DestinatioAccount);
        Assert.Equal(result.Amount, bankingOperations.Amount);
        Assert.Equal(result.Operation, bankingOperations.Operation);
    }

    [Fact]
    public void GetAll()
    {
        var bankingOperations = new[]
            { new BankingOperationsViewModel(), new BankingOperationsViewModel() };
        
        var bankingOperationsRepositoryMock = new Mock<IBankingOperationsRepository>();

        bankingOperationsRepositoryMock.As<IQueryable<BankingOperationsViewModel>>().Setup(m => m.Provider).Returns(bankingOperations.AsQueryable().Provider);
        bankingOperationsRepositoryMock.As<IQueryable<BankingOperationsViewModel>>().Setup(m => m.Expression).Returns(bankingOperations.AsQueryable().Expression);
        bankingOperationsRepositoryMock.As<IQueryable<BankingOperationsViewModel>>().Setup(m => m.ElementType).Returns(bankingOperations.AsQueryable().ElementType);
        bankingOperationsRepositoryMock.As<IQueryable<BankingOperationsViewModel>>().Setup(m => m.GetEnumerator()).Returns(bankingOperations.AsQueryable().GetEnumerator());
    }
}