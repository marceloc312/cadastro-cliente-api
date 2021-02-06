using Api.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Core.Contracts.Repositorys
{
    public interface IEnderecoClienteRepositoryReadOnly
    {
        Task<IEnumerable<EnderecoCliente>> BuscarPorIdClienteAsync(long idCliente);
    }
    public interface IEnderecoClienteRepository
    {
        Task AlterarAsync(EnderecoCliente enderecoCliente);
    }
}
