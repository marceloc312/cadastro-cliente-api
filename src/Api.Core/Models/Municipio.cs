namespace Api.Core.Models
{
    public class Municipio
    {
        public Municipio()
        {
        }

        public Municipio(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
