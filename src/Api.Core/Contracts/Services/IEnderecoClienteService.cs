using Api.Core.Models;
using Api.Core.Validations;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Services
{
    public interface IEnderecoClienteService
    {
        Task<ValidateResult> AlterarEndereco(EnderecoCliente enderecoCliente);
    }
}
