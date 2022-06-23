using System.ComponentModel.DataAnnotations;
using Rede.Domain.Core.Models;

namespace Rede.Domain.Models;

public class BankingOperations : EntityAudit
{
    public BankingOperations(Guid id, string originAccount, string destinatioAccount, double amount, string operation)
    {
        Id = id;
        OriginAccount = originAccount;
        DestinatioAccount = destinatioAccount;
        Amount = amount;
        Operation = operation;
    }

    protected BankingOperations()
    {
    }

    public string OriginAccount { get; private set; }

    public string DestinatioAccount { get; private set; }
        
    public double Amount { get; private set; }

    public string Operation { get; private set; }

}