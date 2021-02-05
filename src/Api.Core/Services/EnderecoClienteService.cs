using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class EnderecoClienteService : IEnderecoClienteService
    {
        readonly ILogger<EnderecoClienteService> _logger;
        readonly IEnderecoClienteRepository _enderecoClienteRepository;

        public EnderecoClienteService(ILogger<EnderecoClienteService> logger, IEnderecoClienteRepository enderecoClienteRepository)
        {
            _logger = logger;
            _enderecoClienteRepository = enderecoClienteRepository;
        }

        public async Task<IEnumerable<EnderecoCliente>> BuscaEnderecosPorIdCliente(long idCliente)
        {
            IEnumerable<EnderecoCliente> result = new List<EnderecoCliente>();
            try
            {
                _logger.LogInformation($"Consultando endereços do cliente {idCliente}, no banco");
                var enderecos = await _enderecoClienteRepository.BuscarPorIdClienteAsync(idCliente);

                _logger.LogInformation($"Endereços consultados com sucesso");

                result = enderecos.Select(endereco => new EnderecoCliente(endereco.Id, endereco.ClienteId, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Cidade, endereco.Pais, endereco.CEP));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao consultar endereços do cliente {idCliente}. {ex}");
            }

            return result;
        }
    }
}
