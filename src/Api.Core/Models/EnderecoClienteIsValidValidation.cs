using Api.Core.Interfaces.Validations;
using Api.Core.Models.Specifications;
using Api.Core.Validations;

namespace Api.Core.Models
{
    public class EnderecoClienteIsValidValidation : Validation<EnderecoCliente>
    {
        public EnderecoClienteIsValidValidation()
        {
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteLogradouroSpec(), EnderecoClienteValidationMessages.LOGRADOURO_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteNumeroSpec(), EnderecoClienteValidationMessages.NUMERO_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteCidadeInformadaSpec(), EnderecoClienteValidationMessages.CAMPO_CIDADE_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClientePaisInformadoSpec(), EnderecoClienteValidationMessages.PAIS_NAO_INFORMADO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteCEPSpec(), EnderecoClienteValidationMessages.CEP_INVALIDO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new EnderecoClienteIdClienteSpec(), EnderecoClienteValidationMessages.ID_CLIENTE_NAO_ATRIBUIDO));
            base.AddRule(new ValidationRule<EnderecoCliente>(new  EnderecoClienteUFPreenchidoSpec(), EnderecoClienteValidationMessages.ESTADO_NAO_INFORMADO));
        }
    }
}
