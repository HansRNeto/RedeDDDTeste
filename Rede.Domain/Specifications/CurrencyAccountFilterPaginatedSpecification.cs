using Rede.Domain.Models;

namespace Rede.Domain.Specifications;

public sealed class CurrencyAccountFilterPaginatedSpecification : BaseSpecification<CurrencyAccount>
{
    public CurrencyAccountFilterPaginatedSpecification(int skip, int take)
        : base(i => true)
    {
        ApplyPaging(skip, take);
    }
}