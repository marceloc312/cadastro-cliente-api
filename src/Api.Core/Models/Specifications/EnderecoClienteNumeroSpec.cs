using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteNumeroSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.Numero) && !string.IsNullOrWhiteSpace(entity.Numero);
        }
    }
}