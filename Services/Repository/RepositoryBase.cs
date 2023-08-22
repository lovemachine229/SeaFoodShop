using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection DbConnect { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            this.Transaction = transaction;
        }
        public int ExecuteStoredProcedure(string storedName, DynamicParameters p)
        {
            return DbConnect.Execute(storedName, p, Transaction, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedName, DynamicParameters p)
        {
            return DbConnect.Query<T>(storedName, p, Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
