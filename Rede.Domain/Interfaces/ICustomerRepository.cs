using Rede.Domain.Models;

namespace Rede.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
        Customer GetByDocument(string document);

    }
}
