namespace Api.Core.DTOs.ACL
{
    public class UFDTO
    {
        public int id { get; set; }
        public string sigla { get; set; }
        public string nome { get; set; }
        public RegiaoDTO regiao { get; set; }
    }

}
