using Dapper;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface ICustomerInfoRepository
    {
        int Create(CustomerInfor obj);
    }

    public class CustomerInfoRepository : RepositoryBase, ICustomerInfoRepository
    {
        public CustomerInfoRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int Create(CustomerInfor obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@GuidId", obj.GuidId);
                p.Add("@PhoneNo", obj.PhoneNo);
                p.Add("@Address", obj.Address);
                p.Add("@FullName", obj.FullName);

                return ExecuteStoredProcedure("CustomerInfo_Create", p);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
