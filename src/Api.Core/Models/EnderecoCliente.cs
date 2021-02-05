namespace Api.Core.Models
{
    public class EnderecoCliente
    {
        public EnderecoCliente()
        {
        }

        public EnderecoCliente(int id,long idCliente, string logradouro, string numero, string complemento, string cidade, string pais, string cEP)
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
    }
    public class EnderecoClienteDTO
    {
        public EnderecoClienteDTO()
        {
        }

        public int Id { get; set; }
        public long ClienteId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
    }
}
