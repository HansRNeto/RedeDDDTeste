using System.Security.Claims;

namespace Rede.Infra.CrossCutting.Identity.Services;

public interface IJwtFactory
    {
        Task<JwtToken> GenerateJwtToken(ClaimsIdentity claimsIdentity);
    }

