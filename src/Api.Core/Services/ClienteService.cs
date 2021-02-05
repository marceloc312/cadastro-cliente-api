using Api.Core.Contracts;
using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class ClienteService : IClienteService
    {
       readonly ILogger<ClienteService> _logger;
        readonly IClienteRepository _clienteRepository;

        public ClienteService(ILogger<ClienteService> logger, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> BuscaPorCPF(string cpf)
        {
            Cliente result = default;
            try
            {
                _logger.LogInformation($"Consultando no banco o cpf {cpf}");
                ClienteDTO clienteDTO = await _clienteRepository.BuscarPorCpfAsync(cpf);
                _logger.LogInformation("Cpf consultado com sucesso");
                return clienteDTO != default ? new Cliente(clienteDTO.Nome, clienteDTO.Cpf) : null;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erro ao consultar o cliente. Cpf {cpf}. {ex}");
            }

            return result;
        }
    }
}
