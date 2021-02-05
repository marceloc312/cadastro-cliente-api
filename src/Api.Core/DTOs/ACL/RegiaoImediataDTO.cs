namespace Api.Core.DTOs.ACL
{
    public class RegiaoImediataDTO
    {
        public int id { get; set; }
        public string nome { get; set; }
        public RegiaoIntermediariaDTO regiaointermediaria { get; set; }
    }

}
