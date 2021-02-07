namespace Api.Core.Models.Specifications
{
    public class EnderecoClienteValidationMessages
    {
        public const string LOGRADOURO_NAO_INFORMADO =@"O [logradouro] não foi informado";
        public const string NUMERO_NAO_INFORMADO = @"O [número] não foi informado.";
        public const string CAMPO_CIDADE_NAO_INFORMADO=@"[Cidade] não informada";
        public const string PAIS_NAO_INFORMADO=@"[País] não informado";
        public const string CEP_INVALIDO = @"[CEP] informado inválido, ele deve conter somente números e ter 8 posições";
        public const string ESTADO_NAO_INFORMADO =@"UF inválida";
        public const string ID_CLIENTE_NAO_ATRIBUIDO="Id do cliente não foi informado";
    }
}