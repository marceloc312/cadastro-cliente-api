using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClientePaisInformadoSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.Pais) && !string.IsNullOrWhiteSpace(entity.Pais);
        }
    }
}