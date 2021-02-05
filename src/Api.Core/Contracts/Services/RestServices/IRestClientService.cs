using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Services.RestServices
{
    public interface IRestClientService
    {
        Task<HttpResponseMessage> GetAsync(string valueForTemplate = null);
    }
}
