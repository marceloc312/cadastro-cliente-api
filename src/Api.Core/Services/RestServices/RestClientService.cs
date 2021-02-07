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
        readonly string _url;
        readonly string _templateResource;

        protected RestClientService(string url, string templateResource)
        {
            _url = url;
            _templateResource = templateResource;
        }

        public async Task<HttpResponseMessage> GetAsync(string valueForTemplate = null)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_url),
            };
            string requestUri = string.Empty;
            if (!string.IsNullOrEmpty(valueForTemplate))
                requestUri = string.Format(_templateResource, valueForTemplate);

            return await httpClient.GetAsync(requestUri);
        }
    }
}

