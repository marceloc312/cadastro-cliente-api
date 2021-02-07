using Api.Core.Contracts.Facades;
using Api.Core.Contracts.Services;
using Api.Core.Contracts.Services.RestServices;
using Api.Core.DTOs;
using Api.Core.ModelConfigs;
using Api.Core.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Core.Facades
{
    public class CepFacade : ICepFacade
    {
        readonly IRestClientCEPService _restClientService;
        readonly ILogger<CepFacade> _log;

        public CepFacade(IRestClientCEPService restClientService, ILogger<CepFacade> log)
        {
            _restClientService = restClientService;
            _log = log;
        }

        public async Task<EnderecoCep> Buscar(string cep)
        {
            EnderecoCep result = default;
            try
            {
                if (!Regex.IsMatch(cep, @"^\d{8}$") && !Regex.IsMatch(cep, @"^\d{5}-\d{3}$"))
                    throw new Exception($"CEP {cep} inválido");

                _log.LogInformation($"Enviando requisição GET do CEP {cep}.");
                var response = await _restClientService.GetAsync(cep);
                var mensagemDeResposta = await response.Content.ReadAsStringAsync();
                _log.LogInformation($"Consulta executada com StatusCode:{response.StatusCode} Conteudo de retorno: {mensagemDeResposta}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _log.LogInformation("Transformando resposta em objeto CEP");
                    var cepRetorno = JsonConvert.DeserializeObject<CepDTO>(mensagemDeResposta);
                    result = cepRetorno.ToEnderecoCep();
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"Erro ao consultar o CEP {cep}. {ex}");
            }

            return result;
        }
    }
}
