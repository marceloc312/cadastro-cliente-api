using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.ModelConfigs;
using Microsoft.Extensions.Options;

namespace Api.Core.Services.RestServices
{
    public class RestClientCEPService : RestClientService, IRestClientCEPService
    {
        public RestClientCEPService(IOptions<ParametroConsultaCEP> parametroConsultaCep) : base(parametroConsultaCep.Value.Url, parametroConsultaCep.Value.TemplateResource)
        {
        }
    }
}
