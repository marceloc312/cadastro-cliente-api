using Api.Core.Interfaces.Validations;
using Api.Core.Validations;

namespace Api.Core.Models
{
    public class EnderecoCliente : ISelfValidation
    {
        readonly EnderecoClienteIsValidValidation _validation = new EnderecoClienteIsValidValidation();
        public EnderecoCliente()
        {
        }

        public EnderecoCliente(int id, long idCliente, string logradouro, string numero, string complemento, string cidade, string pais, string cEP)
        {
            Id = id;
            ClienteId = idCliente;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Pais = pais;
            CEP = cEP;
        }

        public int Id { get; set; }
        public long ClienteId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }

        public ValidateResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            ValidationResult = _validation.Valid(this);
            return ValidationResult.IsValid;
        }
        public bool IsValidoForUpdate()
        {
            ValidationResult = _validation.Valid(this);
            if (Id < 1)
                ValidationResult.Add("Endereço sem Id informado para alteração");

            return ValidationResult.IsValid;
        }
    }
}
