using System.Security.Claims;

namespace Rede.Domain.Interfaces;

public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }

