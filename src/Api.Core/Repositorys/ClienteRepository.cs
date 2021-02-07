using Api.Core.Contracts.Repositorys;
using Api.Core.Models;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

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
        public const string SELECT_CLIENTE_POR_CPF = @"SELECT c.id Id, c.nome Nome, c.cpf CPF FROM cliente c WHERE c.cpf =  @cpf";
    }
}
