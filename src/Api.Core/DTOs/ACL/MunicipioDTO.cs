namespace Api.Core.DTOs.ACL
{
    public class MunicipioDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public MicrorregiaoDTO microrregiao { get; set; }
        public RegiaoImediataDTO regiaoimediata { get; set; }
    }
}
