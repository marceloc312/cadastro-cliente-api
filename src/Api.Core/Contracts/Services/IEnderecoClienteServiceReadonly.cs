using Api.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Services
{
    public interface IEnderecoClienteServiceReadonly
    {
        Task<IEnumerable<EnderecoCliente>> BuscaEnderecosPorIdCliente(long idCliente);
    }
}
