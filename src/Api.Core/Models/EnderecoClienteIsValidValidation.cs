using Api.Core.Interfaces.Validations;
using Api.Core.Models.Specifications;
using Api.Core.Validations;

namespace Api.Core.Models
{
    public class EnderecoClienteIsValidValidation : Validation<EnderecoCliente>
    {
        public EnderecoClienteIsValidValidation()
        {
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteLogradouroVazioSpec(), EnderecoClienteValidationMessages.LOGRADOURO_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteNumeroVazioSpec(), EnderecoClienteValidationMessages.NUMERO_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteCidadeNaoInformadaSpec(), EnderecoClienteValidationMessages.CAMPO_CIDADE_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClientePaisNaoInformadoSpec(), EnderecoClienteValidationMessages.PAIS_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteCEPInvalidoSpec(), EnderecoClienteValidationMessages.CEP_INVALIDO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteSemIdClienteSpec(), EnderecoClienteValidationMessages.ID_CLIENTE_NAO_ATRIBUIDO));            
        }

    }
}
