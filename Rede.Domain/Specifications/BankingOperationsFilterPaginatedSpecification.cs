using Rede.Domain.Models;

namespace Rede.Domain.Specifications;

public sealed class BankingOperationsFilterPaginatedSpecification : BaseSpecification<BankingOperations>
{
    public BankingOperationsFilterPaginatedSpecification(int skip, int take)
        : base(i => true)
    {
        ApplyPaging(skip, take);
    }
}