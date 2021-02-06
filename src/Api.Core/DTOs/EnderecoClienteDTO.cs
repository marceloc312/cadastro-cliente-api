namespace Api.Core.DTOs
{
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
