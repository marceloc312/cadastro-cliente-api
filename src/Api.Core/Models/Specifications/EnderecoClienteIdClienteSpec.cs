using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteIdClienteSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return entity.ClienteId > 0;
        }
    }
}