using Api.Core.Models;
using Api.Core.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Services
{
    public interface IEnderecoClienteService
    {
        Task<bool> AlterarEndereco(EnderecoCliente enderecoCliente);
        Task<IEnumerable<EnderecoCliente>> BuscaEnderecosPorIdClienteAsync(long idCliente);
        Task<EnderecoCliente> BuscaEnderecoPorIdAsync(long idCliente,int idEndereco);
    }
}
