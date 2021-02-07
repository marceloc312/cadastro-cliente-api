namespace Api.Core.Models
{
    public class EstadoUF
    {
        public EstadoUF()
        {
        }

        public EstadoUF(int id, string sigla, string nome)
        {
            Id = id;
            Sigla = sigla;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
    }
}
