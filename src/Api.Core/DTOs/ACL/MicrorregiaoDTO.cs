namespace Api.Core.DTOs.ACL
{
    public class MicrorregiaoDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public MesorregiaoDTO mesorregiao { get; set; }
    }

}
