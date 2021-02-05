using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.ModelConfigs;

namespace Api.Core.Services.RestServices
{
    public class RestClientCEPService : RestClientService, IRestClientCEPService
    {
        public RestClientCEPService(ACLRestConfig aCLRestConfig) : base(aCLRestConfig)
        {
        }
    }
}
