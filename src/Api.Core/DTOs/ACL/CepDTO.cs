using Api.Core.Models;
using System;

namespace Api.Core.DTOs
{
    public class CepDTO
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }

        internal EnderecoCep ToEnderecoCep()
        {
            if (!string.IsNullOrEmpty(logradouro) && !string.IsNullOrEmpty(uf) && !string.IsNullOrEmpty(localidade))
                return new EnderecoCep(cep, logradouro, complemento, bairro, localidade, uf);

            return null;
        }
    }

}
