using Api.Core.Contracts.Services.RestServices;
using Api.Core.ModelConfigs;
using Microsoft.Extensions.Options;

namespace Api.Core.Services.RestServices
{
    public class RestClientEstadoService : RestClientService, IRestClientEstadoService
    {
        public RestClientEstadoService(IOptions<ParametroRestConsultaEstado> parametroRest) : base(parametroRest.Value)
        {
        }
    }
}
