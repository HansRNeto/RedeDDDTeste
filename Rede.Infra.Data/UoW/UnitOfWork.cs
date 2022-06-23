using Rede.Domain.Interfaces;
using Rede.Infra.Data.Context;

namespace Rede.Infra.Data.UoW;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly RedeContext _context;

        public UnitOfWork(RedeContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }