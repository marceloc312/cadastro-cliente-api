using Api.Core.Interfaces.Validations;
using System.Text.RegularExpressions;

namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteCEPSpec : ISpecification<EnderecoCliente>
    {
        public bool IsSatisfiedBy(EnderecoCliente entity)
        {
            return !string.IsNullOrEmpty(entity.CEP) 
                && !string.IsNullOrWhiteSpace(entity.CEP)  
                && (Regex.IsMatch(entity.CEP, @"^\d{8}$") || Regex.IsMatch(entity.CEP, @"^\d{5}-\d{3}$"));
        }
    }
}