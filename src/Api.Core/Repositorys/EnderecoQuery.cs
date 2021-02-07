namespace Api.Core.Repositorys
{
    internal static class EnderecoQuery
    {
        public const string SELECT_ENDERECOS_POR_ID_CLIENTE = @"
            SELECT
                ce.Id,
                ce.cliente_id ClienteId,
                ce.logradouro Logradouro,
                ce.numero Numero,
                ce.complemento Complemento,
                ce.cidade Cidade,
                ce.estado Estado,
                ce.pais Pais,
                ce.cep CEP
            FROM cliente_endereco ce 
            WHERE
                ce.cliente_id = @idCliente";

        public const string SELECT_ENDERECOS_POR_ID= @"
            SELECT
                ce.Id,
                ce.cliente_id ClienteId,
                ce.logradouro Logradouro,
                ce.numero Numero,
                ce.complemento Complemento,
                ce.cidade Cidade,
                ce.estado Estado,
                ce.pais Pais,
                ce.cep CEP
            FROM cliente_endereco ce 
            WHERE
                ce.id = @id AND ce.cliente_id = @idCliente";
    }
}
