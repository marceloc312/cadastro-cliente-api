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
    public class EnderecoClienteServiceReadOnly : IEnderecoClienteServiceReadonly
    {
        readonly ILogger<EnderecoClienteServiceReadOnly> _logger;
        readonly IEnderecoClienteRepositoryReadOnly _enderecoClienteRepository;

        public EnderecoClienteServiceReadOnly(ILogger<EnderecoClienteServiceReadOnly> logger, IEnderecoClienteRepositoryReadOnly enderecoClienteRepository)
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
                result = await _enderecoClienteRepository.BuscarPorIdClienteAsync(idCliente);
                _logger.LogInformation($"Endereços consultados com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao consultar endereços do cliente {idCliente}. {ex}");
            }

            return result;
        }
    }
}
