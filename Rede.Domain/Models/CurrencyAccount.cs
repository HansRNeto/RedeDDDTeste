using Rede.Domain.Core.Models;

namespace Rede.Domain.Models;

public class CurrencyAccount : EntityAudit
{
    public CurrencyAccount(Guid id, int numberAccount, int digit, double balance, Guid customerId)
    {
        Id = id;
        NumberAccount = numberAccount;
        Digit = digit;
        Balance = balance;
        CustomerId = customerId;
    }

    protected CurrencyAccount()
    {
    }

    public int NumberAccount { get; private set; }

    public int Digit { get; private set; }
        
    public double Balance { get; private set; }

    public Guid CustomerId { get; private set; }

    //EF Navigation
    public Customer? Customer { get; set; }
}