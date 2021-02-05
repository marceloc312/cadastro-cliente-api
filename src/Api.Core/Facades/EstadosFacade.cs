using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Facades
{
    public class EstadosFacade : IEstadosFacade
    {
        readonly ILogger<EstadosFacade> _logger;
        readonly IRestClientEstadosService _restClientEstadosService;

        public EstadosFacade(ILogger<EstadosFacade> logger, IRestClientEstadosService restClientEstadosService)
        {
            _logger = logger;
            _restClientEstadosService = restClientEstadosService;
        }

        public async Task<IEnumerable<UFDTO>> ListarUFs()
        {
            IEnumerable<UFDTO> result = new List<UFDTO>();
            try
            {
                _logger.LogInformation($"Enviando requisição GET dos Estados.");
                var response = await _restClientEstadosService.GetAsync();
                var mensagemDeResposta = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Consulta executada com StatusCode: {response.StatusCode} Conteudo de retorno: {mensagemDeResposta}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Transformando resposta em objeto {nameof(UFDTO)}");
                    result = JsonConvert.DeserializeObject<IEnumerable<UFDTO>>(mensagemDeResposta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consultar o Estados. {ex}");
            }

            return result;
        }

        public async Task<IEnumerable<MunicipioDTO>> ListarMunicipiosPorEstado(int idEstado)
        {
            IEnumerable<MunicipioDTO> result = new List<MunicipioDTO>();
            try
            {
                _logger.LogInformation($"Enviando requisição GET dos Municípios do Estado {idEstado}.");
                var response = await _restClientEstadosService.GetAsync();
                var mensagemDeResposta = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Consulta executada com StatusCode: {response.StatusCode} Conteudo de retorno: {mensagemDeResposta}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Transformando resposta em objeto {nameof(MunicipioDTO)}");
                    result = JsonConvert.DeserializeObject<IEnumerable<MunicipioDTO>>(mensagemDeResposta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consultar o Estados. {ex}");
            }

            return result;
        }
    }
}
