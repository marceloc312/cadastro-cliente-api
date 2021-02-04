using Api.Core.Contracts;
using Api.Core.Contracts.Repositorys;
using Api.Core.Contracts.Services;
using Api.Core.DTOs;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class ClienteService : IClienteService
    {
        readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> BuscaPorCPF(string cpf)
        {
            ClienteDTO clienteDTO= await _clienteRepository.FindAsync(cpf);

            return clienteDTO != default ? new Cliente(clienteDTO): null;
        }
    }
}
