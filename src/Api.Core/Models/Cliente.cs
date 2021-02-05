using Api.Core.DTOs;
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

        public Cliente(string nome, string noCpfme)
        {
            Nome = nome;
            NoCpfme = noCpfme;
        }

        public string Nome { get; set; }
        public string NoCpfme { get; set; }
    }
}
