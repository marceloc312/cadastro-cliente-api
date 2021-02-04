using Api.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Repositorys
{
    public interface IClienteRepository
    {
        Task<ClienteDTO> FindAsync(string cpf);
    }
}
