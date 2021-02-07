namespace Api.Core.Models
{
    public class EnderecoCep
    {
        public EnderecoCep()
        {
        }

        public EnderecoCep(string cep, string logradouro, string complemento, string bairro, string localidade, string uf)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }
}
