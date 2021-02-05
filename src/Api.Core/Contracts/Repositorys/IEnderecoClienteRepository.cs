using Api.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Api.Core.Contracts.Repositorys
{
    public interface IEnderecoClienteRepository
    {
        Task<IEnumerable<EnderecoClienteDTO>> BuscarPorIdClienteAsync(long idCliente);
    }
}
