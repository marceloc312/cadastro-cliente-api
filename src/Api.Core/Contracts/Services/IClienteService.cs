using Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Services
{
    public interface IClienteService
    {
        Task<Cliente> BuscaPorCPF(string cpf);
    }
}
