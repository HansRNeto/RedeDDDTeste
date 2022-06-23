using AutoMapper;
using Moq;
using Rede.Domain.Interfaces;
using Rede.Service.Services;
using Rede.Service.ViewModels;
using Xunit;

namespace Rede.Tests.UnitTests.Services;

public class CustomerAppServiceTests
    {
        [Fact]
        public void GetById()
        {
            var customer = new Domain.Models.Customer(new Guid(), "Alan", "alab@test.com", new DateTime(), "362.982.658-05");

            var customerRepositoryMock = new Mock<ICustomerRepository>();
            
            customerRepositoryMock.Setup(x => x.GetById(customer.Id))
                .Returns(customer);
            
            var mapperMock = new Mock<IMapper>();
            
            mapperMock.Setup(x => x.Map<CustomerViewModel>(customer)).Returns(new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                BirthDate = customer.BirthDate,
                Document = customer.Document
            });

            var sut = new CustomerAppService(mapperMock.Object, customerRepositoryMock.Object, null!);
            var result = sut.GetById(customer.Id);

            Assert.Equal(result.Id, customer.Id);
            Assert.Equal(result.Name, customer.Name);
            Assert.Equal(result.Email, customer.Email);
            Assert.Equal(result.BirthDate, customer.BirthDate);
            Assert.Equal(result.Document, customer.Document);
        }
        
        [Fact]
        public void GetAll()
        {
            var customer = new[]
                { new CustomerViewModel(), new CustomerViewModel() };
        
            var customerRepositoryMock = new Mock<ICustomerRepository>();

            customerRepositoryMock.As<IQueryable<CustomerViewModel>>().Setup(m => m.Provider).Returns(customer.AsQueryable().Provider);
            customerRepositoryMock.As<IQueryable<CustomerViewModel>>().Setup(m => m.Expression).Returns(customer.AsQueryable().Expression);
            customerRepositoryMock.As<IQueryable<CustomerViewModel>>().Setup(m => m.ElementType).Returns(customer.AsQueryable().ElementType);
            customerRepositoryMock.As<IQueryable<CustomerViewModel>>().Setup(m => m.GetEnumerator()).Returns(customer.AsQueryable().GetEnumerator());
        }
    }
