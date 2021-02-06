using Api.Core.DTOs;
using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Repositorys
{
    public interface IClienteRepository
    {
        Task<Cliente> BuscarPorCpfAsync(string cpf);
    }
}
