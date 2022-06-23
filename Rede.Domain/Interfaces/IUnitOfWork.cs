namespace Rede.Domain.Interfaces;

    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }

