using Api.Core.DTOs;
using Api.Core.Interfaces.Validations;
using Api.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.Models
{
    public class Cliente : ISelfValidation
    {
        readonly ClienteIsValidValidation _validation = new ClienteIsValidValidation();
        public Cliente()
        {
        }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }
        public long IdCliente { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public ValidateResult ValidationResult { get; private set; }

        public bool IsValid()
        {
           ValidationResult = _validation.Valid(this);
            return ValidationResult.IsValid;
        }
    }
}
