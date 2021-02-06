using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.Models;
using Api.Core.Validations;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<ValidateResult> AlterarEndereco(EnderecoCliente enderecoCliente)
        {
            var result = new ValidateResult();
            try
            {
                if (!enderecoCliente.IsValidoForUpdate())
                    return enderecoCliente.ValidationResult;

                _logger.LogInformation($"Alterando endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}");
                await _enderecoClienteRepository.AlterarAsync(enderecoCliente);
                _logger.LogInformation($"Endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId} alterado com sucesso");
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid();
                _logger.LogError($"ID de erro: {guid}. Erro ao alterar o endereço {enderecoCliente.Id} do cliente {enderecoCliente.ClienteId}. { ex}");
                result.Add($"Não foi possível efitivar a transação com sucesso. ID: {guid}");
            }
            return result;
        }
    }
}
