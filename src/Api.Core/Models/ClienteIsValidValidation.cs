using Api.Core.Models.Specifications;
using Api.Core.Validations;

namespace Api.Core.Models
{
    public class ClienteIsValidValidation : Validation<Cliente>
    {
        public ClienteIsValidValidation()
        {
            base.AddRule(new ValidationRule<Cliente>(new ClienteCpfValidoSpec(), ClienteValidationMessages.CPF_INVALIDO));
        }
    }
}
