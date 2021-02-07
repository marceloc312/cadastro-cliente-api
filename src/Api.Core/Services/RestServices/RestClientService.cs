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
        readonly ParametroConsultaRestTerceirosTemplate _parametroRest;

        protected RestClientService(ParametroConsultaRestTerceirosTemplate parametroRest)
        {
            _parametroRest = parametroRest;
        }

        public async Task<HttpResponseMessage> GetAsync(string valueForTemplate = null)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_parametroRest.Url),
            };
            string requestUri = string.Empty;
            if (!string.IsNullOrEmpty(valueForTemplate))
                requestUri = string.Format(_parametroRest.TemplateResource, valueForTemplate);

            return await httpClient.GetAsync(requestUri);
        }
    }
}

