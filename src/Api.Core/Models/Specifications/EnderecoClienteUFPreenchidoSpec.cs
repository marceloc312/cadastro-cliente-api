using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteUFPreenchidoSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.Estado) & entity.Estado.Length == 2;
        }
    }
}