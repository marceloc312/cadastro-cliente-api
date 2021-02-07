using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.ModelConfigs;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Core.Services.RestServices
{
    public class RestClientCEPService : RestClientService, IRestClientCEPService
    {
        public RestClientCEPService(IOptions<ParametroRestConsultaCEP> parametroRest) : base(parametroRest.Value)
        {
        }
    }
}
