using AutoMapper;
using Moq;
using Rede.Domain.Interfaces;
using Rede.Service.Services;
using Rede.Service.ViewModels;
using Xunit;

namespace Rede.Tests.UnitTests.Services;

public class CurrencyAccountAppServiceTests
{
    [Fact]
    public void GetById()
    {
        // Arrange
        var currencyAccount = new Domain.Models.CurrencyAccount(new Guid(), 000002, 00, 500, new Guid());

        var currencyAccountRepositoryMock = new Mock<ICurrencyAccountRepository>();
        
        currencyAccountRepositoryMock.Setup(x => x.GetById(currencyAccount.Id))
            .Returns(currencyAccount);
        
        var mapperMock = new Mock<IMapper>();
        
        mapperMock.Setup(x => x.Map<CurrencyAccountViewModel>(currencyAccount)).Returns(new CurrencyAccountViewModel
        {
            Id = currencyAccount.Id,
            NumberAccount = currencyAccount.NumberAccount,
            Digit = currencyAccount.Digit,
            Balance = currencyAccount.Balance,
            CustomerId = currencyAccount.CustomerId
        });

        // Act
        var sut = new CurrencyAccountAppService(mapperMock.Object, currencyAccountRepositoryMock.Object, null!);
        var result = sut.GetById(currencyAccount.Id);

        // Assert
        Assert.Equal(result.Id, currencyAccount.Id);
        Assert.Equal(result.NumberAccount, currencyAccount.NumberAccount);
        Assert.Equal(result.Digit, currencyAccount.Digit);
        Assert.Equal(result.Balance, currencyAccount.Balance);
        Assert.Equal(result.CustomerId, currencyAccount.CustomerId);
    }
    
    [Fact]
    public void GetAll()
    {
        var currencyAccount = new[]
            { new CurrencyAccountViewModel(), new CurrencyAccountViewModel() };
        
        var currencyAccountRepositoryMock = new Mock<ICurrencyAccountRepository>();

        currencyAccountRepositoryMock.As<IQueryable<CurrencyAccountViewModel>>().Setup(m => m.Provider).Returns(currencyAccount.AsQueryable().Provider);
        currencyAccountRepositoryMock.As<IQueryable<CurrencyAccountViewModel>>().Setup(m => m.Expression).Returns(currencyAccount.AsQueryable().Expression);
        currencyAccountRepositoryMock.As<IQueryable<CurrencyAccountViewModel>>().Setup(m => m.ElementType).Returns(currencyAccount.AsQueryable().ElementType);
        currencyAccountRepositoryMock.As<IQueryable<CurrencyAccountViewModel>>().Setup(m => m.GetEnumerator()).Returns(currencyAccount.AsQueryable().GetEnumerator());
    }
}