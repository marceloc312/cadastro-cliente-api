using Api.Core.Contracts.Repositorys;
using Api.Core.DTOs;
using Api.Core.Models;
using Dapper;
using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Api.Core.Repositorys
{
    public class ClienteRepository :BaseRepository, IClienteRepository
    {
        public ClienteRepository(IConnectionFactoryDatabase connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<Cliente> BuscarPorCpfAsync(string cpf)
        {
            var conn = _connectionFactory.Connection();
            var query = await conn.QueryAsync<Cliente>(sql: ClienteQuery.SELECT_CLIENTE_POR_CPF, param: new { cpf });
            
            var result = query.LastOrDefault();
            return result;
        }
    }
    internal static class ClienteQuery
    {
        public const string SELECT_CLIENTE_POR_CPF = @"                
                SELECT
	                c.id IdCliente,
	                c.nome Nome,
	                c.cpf CPF
                FROM
	                cliente c
                WHERE
	                c.cpf =  @cpf";
    }
}
