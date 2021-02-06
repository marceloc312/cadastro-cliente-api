namespace Api.Core.Models.Specifications
{
    internal class EnderecoClienteValidationMessages
    {
        internal const string LOGRADOURO_NAO_INFORMADO =@"O [logradouro] não foi informado";
        internal const string NUMERO_NAO_INFORMADO = @"O [número] não foi informado.";
        internal const string CAMPO_CIDADE_NAO_INFORMADO=@"[Cidade] não informada";
        internal const string PAIS_NAO_INFORMADO=@"[País] não informado";
        internal const string CEP_INVALIDO = @"[CEP] informado inválido";
        internal static string ID_CLIENTE_NAO_ATRIBUIDO="Id do cliente não foi informado";
    }
}