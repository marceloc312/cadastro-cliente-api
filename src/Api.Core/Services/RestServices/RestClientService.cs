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

        public async Task<HttpResponseMessage> GetAsync(string valueForTemplate = null)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_aCLRestConfig.Url),
            };
            string requestUri = string.Empty;
            if (!string.IsNullOrEmpty(valueForTemplate))
                requestUri = string.Format(_aCLRestConfig.TemplateResource, valueForTemplate);

            return await httpClient.GetAsync(requestUri);
        }
    }
}

