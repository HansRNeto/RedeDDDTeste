using Refit;

namespace Rede.Domain.Services.Http;

public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }

