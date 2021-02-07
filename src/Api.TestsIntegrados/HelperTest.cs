using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.TestsIntegrados
{
    public static class HelperTest
    {
        public static async Task<bool> PingDeVerificacaoServicoLocalizacaoEstadosMunicipios()
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://servicodados.ibge.gov.br/api/v1/localidades/estados/"),
            };
            var requestApiTerceiros = await httpClient.GetAsync("35/municipios");
            return requestApiTerceiros.IsSuccessStatusCode;
        }
    }
}
