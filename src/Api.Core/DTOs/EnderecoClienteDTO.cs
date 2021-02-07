using Api.Core.Models;
using System;

namespace Api.Core.DTOs
{
    public class EnderecoClienteDTO
    {
        public EnderecoClienteDTO()
        {
        }

        public EnderecoClienteDTO(EnderecoCliente endereco)
        {

            Id = endereco.Id;
            Logradouro = endereco.Logradouro;
            Numero = endereco.Numero;
            Complemento = endereco.Complemento;
            Cidade = endereco.Cidade;
            Estado = endereco.Estado;
            Pais = endereco.Pais;
            CEP = endereco.CEP;
        }
        public EnderecoClienteDTO(int id, string logradouro, string numero, string complemento, string cidade, string estado, string pais, string cEP)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            CEP = cEP;
        }

        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }

        public EnderecoCliente ToEnderecoCliente(long idCliente, int idEndereco)
        {
            return new EnderecoCliente(idEndereco, idCliente, Logradouro, Numero, Complemento, Cidade, Estado, Pais, CEP);
        }
    }
}
