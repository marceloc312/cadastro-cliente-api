using System.Data;
using System.Threading.Tasks;

namespace Api.Core.Contracts.Repositorys
{
    public interface IConnectionFactoryDatabase
    {
        IDbConnection Connection();
        Task OpenConnectionAsync();
        void CloseConnection();
        IDbTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IDbTransaction CurrentTransaction();
    }
}
