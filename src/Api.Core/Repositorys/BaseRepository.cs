using Api.Core.Contracts.Repositorys;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Api.Core.Repositorys
{
    public abstract class BaseRepository
    {
        protected readonly IConnectionFactoryDatabase _connectionFactory;
        protected readonly IDbConnection connection;

        public BaseRepository(IConnectionFactoryDatabase connectionFactory)
        {
            _connectionFactory = connectionFactory;

            connection = _connectionFactory.Connection();
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param)
        {
            var command = CreateDefinition(sql, param);

            return await connection.ExecuteScalarAsync<T>(command);
        }

        public async Task<int> ExecuteAsync(string sql, object param)
        {
            var command = CreateDefinition(sql, param);

            return await connection.ExecuteAsync(command);
        }

        public async Task ExecuteAsyncComTransaction(string sql, object param)
        {
            _connectionFactory.BeginTransaction();

            var command = CreateDefinition(sql, param);

            await connection.ExecuteAsync(command);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param)
        {
            var command = CreateDefinition(sql, param);

            return await connection.QueryAsync<T>(command);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
        {
            return await connection.QueryAsync<T>(command);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param)
        {
            var command = CreateDefinition(sql, param);

            return await connection.QueryFirstOrDefaultAsync<T>(command);
        }

        public async Task<IDataReader> ExecuteReader(string sql, object param)
        {
            var command = CreateDefinition(sql, param);

            return await connection.ExecuteReaderAsync(command);
        }

        public async Task<int> ExecuteIdentity(string sql, string identityColumn, DynamicParameters param)
        {
            await _connectionFactory.Connection().ExecuteAsync(sql, param);

            return param.Get<int>(identityColumn);
        }

        public void CommitTransaction() => _connectionFactory.CommitTransaction();

        public void RollbackTransaction() => _connectionFactory.RollbackTransaction();

        private CommandDefinition CreateDefinition(string sql, object param)
        {
            if (_connectionFactory.CurrentTransaction() == null)
            {
                return new CommandDefinition(sql, param);
            }
            else
            {
                return new CommandDefinition(sql, param, _connectionFactory.CurrentTransaction());
            }
        }
    }
}
