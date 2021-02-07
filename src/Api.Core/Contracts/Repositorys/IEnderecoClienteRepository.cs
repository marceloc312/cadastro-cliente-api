using Api.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Core.Contracts.Repositorys
{
    public interface IEnderecoClienteRepository
    {
        Task AlterarAsync(EnderecoCliente enderecoCliente);
        Task<IEnumerable<EnderecoCliente>> BuscarPorIdClienteAsync(long idCliente);
        Task<EnderecoCliente> BuscaEnderecoPorIdAsync(long idCliente,int idEndereco);
    }
}
