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
    public interface IRoleRepository
    {
        int Create(Role obj);

        IEnumerable<Role> ListAll();

        IEnumerable<Role> ListAllWithCount(int exclude = 0);

        IEnumerable<Role> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);

        Role ViewDetail(int id);

        int Update(Role obj);

        int Delete(int id);

        bool ChangeStatus(int id);
    }

    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public bool ChangeStatus(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure("Role_ChangeStatus", p);

                return rs > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Create(Role obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Name", obj.Name);
                p.Add("@Description", obj.Description);
                p.Add("@Status", obj.Status);

                ExecuteStoredProcedure("Role_Create", p);

                return p.Get<int>("@Id");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                return ExecuteStoredProcedure("Role_Delete", p);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Role> ListAll()
        {
            try
            {
                return ExecuteStoredProcedure<Role>("Role_ListAll", null);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Role> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<Role>("Role_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Role> ListAllWithCount(int exclude = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@exclude", exclude);

                var rs = ExecuteStoredProcedure<Role>("Role_ListAllWithCountProduct", p);

                return rs.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(Role obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", obj.Id, dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Name", obj.Name);
                p.Add("@Description", obj.Description);
                p.Add("@Status", obj.Status);

                ExecuteStoredProcedure("Role_Update", p);

                return p.Get<int>("@Id");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Role ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure<Role>("Role_ViewDetail", p);
                return rs.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
