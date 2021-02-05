using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.ModelConfigs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Core.Services.RestServices
{
    public abstract class RestClientService : IRestClientService
    {
        readonly ACLRestConfig _aCLRestConfig;

        public RestClientService(ACLRestConfig aCLRestConfig)
        {
            _aCLRestConfig = aCLRestConfig;
        }

        public async Task<HttpResponseMessage> GetAsync(string parameterValue)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_aCLRestConfig.Url),
            };

            return await httpClient.GetAsync(string.Format(_aCLRestConfig.TemplateResource, parameterValue));
        }
    }
}
