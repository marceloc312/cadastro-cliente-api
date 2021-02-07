using Api.Core.DTOs;
using Api.Core.Interfaces.Validations;
using Api.Core.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.Models
{
    public class Cliente 
    {
        public Cliente()
        {
        }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            CPF = cpf;
        }
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
    }
}
