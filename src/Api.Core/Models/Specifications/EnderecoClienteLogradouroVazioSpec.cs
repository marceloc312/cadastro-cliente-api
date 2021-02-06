using Api.Core.Interfaces.Validations;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteLogradouroVazioSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.Logradouro) && !string.IsNullOrWhiteSpace(entity.Logradouro);
        }
    }
    internal class EnderecoClienteSemIdClienteSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return entity.ClienteId > 0;
        }
    }
}