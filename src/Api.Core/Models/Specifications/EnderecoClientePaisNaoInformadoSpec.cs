using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClientePaisNaoInformadoSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.Pais) && !string.IsNullOrWhiteSpace(entity.Pais);
        }
    }
}