using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IUtilityRepository
    {
        int CountProduct();

        int CountCustomer();

        int CountOrder();

        decimal CountRevenue();
    }

    public class UtilityRepository : RepositoryBase, IUtilityRepository
    {
        public UtilityRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int CountCustomer()
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Utility_CountCustomer", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CountOrder()
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Utility_CountOrder", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CountProduct()
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Utility_CountProduct", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal CountRevenue()
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Utility_CountRevenue", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
