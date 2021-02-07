using Api.Core.Contracts.Repositorys;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Api.Core.Repositorys
{
    public class ConnectionFactoryDatabase : IConnectionFactoryDatabase, IDisposable
    {
        private readonly MySqlConnection _connection;
        private MySqlTransaction _transaction;
        private bool _isTransactionOpen;
        private bool disposedValue = false; // To detect redundant calls

        public ConnectionFactoryDatabase(string connectionString, bool openConnection)
        {
            _connection = new MySqlConnection(connectionString);

            if (openConnection)
            {
                _connection.Open();
            }
        }

        public IDbConnection Connection() => _connection;

        public async Task OpenConnectionAsync() => await _connection.OpenAsync();

        public void OpenConnection() => _connection.Open();

        public IDbTransaction BeginTransaction()
        {
            if (_transaction == null)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    throw new Exception("A conexão com o banco não esta aberta.");
                }

                _transaction = _connection.BeginTransaction();
            }

            _isTransactionOpen = true;

            return _transaction;
        }

        public void CommitTransaction()
        {
            if (_isTransactionOpen)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }

            _isTransactionOpen = false;
        }

        public void RollbackTransaction()
        {
            if (_isTransactionOpen)
            {
                _transaction.Rollback();
            }
        }

        public void CloseConnection()
        {
            _connection.Close();
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbTransaction CurrentTransaction()
        {
            return _transaction;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    CloseConnection();
                }

                disposedValue = true;
            }
        }
    }
}
