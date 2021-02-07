namespace Api.Core.Repositorys
{
    internal static class ClienteQuery
    {
        public const string SELECT_CLIENTE_POR_CPF = @"SELECT c.id Id, c.nome Nome, c.cpf CPF FROM cliente c WHERE c.cpf =  @cpf";
    }
}
