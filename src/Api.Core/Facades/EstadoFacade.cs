using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.DTOs;
using Api.Core.DTOs.ACL;
using Api.Core.ModelConfigs;
using Api.Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Facades
{
    public class EstadoFacade : IEstadoFacade
    {
        readonly ILogger<EstadoFacade> _logger;
        readonly IRestClientEstadoService _restClientEstadosService;
        readonly OrdenacaoEstados _ordenacaoEstados;

        public EstadoFacade(ILogger<EstadoFacade> logger, IOptions<OrdenacaoEstados> ordenacaoEstados, IRestClientEstadoService restClientEstadosService)
        {
            _logger = logger;
            _ordenacaoEstados = ordenacaoEstados.Value;
            _restClientEstadosService = restClientEstadosService;
        }

        public async Task<IEnumerable<EstadoUF>> ListarUFs()
        {
            IEnumerable<EstadoUF> result = new List<EstadoUF>();
            try
            {
                _logger.LogInformation($"Enviando requisição GET dos Estados.");
                var response = await _restClientEstadosService.GetAsync();
                var mensagemDeResposta = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Consulta executada com StatusCode: {response.StatusCode} Conteudo de retorno: {mensagemDeResposta}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Transformando resposta em objeto {nameof(UFDTO)}");
                    var ufsDTO = JsonConvert.DeserializeObject<IEnumerable<UFDTO>>(mensagemDeResposta);
                    var ufs = ufsDTO.Select(s => new EstadoUF(s.id, s.sigla, s.nome));
                    result = Ordenar(ufs);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consultar o Estados. {ex}");
            }

            return result;
        }

        private IEnumerable<EstadoUF> Ordenar(IEnumerable<EstadoUF> ufs)
        {

            var ufsOrdenados = (_ordenacaoEstados.Sentido == "Asc" ? ufs.OrderBy(x => x.Sigla) : ufs.OrderByDescending(x => x.Sigla)).ToList();
            foreach (var ufRank in _ordenacaoEstados.Rank.Reverse())
            {
                var uf = ufsOrdenados.FirstOrDefault(x => x.Sigla == ufRank);
                int indexUf = ufsOrdenados.FindIndex(x => x.Sigla == ufRank);
                ufsOrdenados.RemoveAt(indexUf);
                ufsOrdenados.Insert(0, uf);
            }

            return ufsOrdenados;
        }

        public async Task<IEnumerable<MunicipioDTO>> ListarMunicipiosPorEstado(int idEstado)
        {
            IEnumerable<MunicipioDTO> result = new List<MunicipioDTO>();
            try
            {
                _logger.LogInformation($"Enviando requisição GET dos Municípios do Estado {idEstado}.");
                var response = await _restClientEstadosService.GetAsync(idEstado.ToString());
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
