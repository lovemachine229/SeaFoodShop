using Dapper;
using Services.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IEmployeeRepository
    {
        Employee Login(LoginViewModel obj);

        int Create(Employee obj);

        IEnumerable<EmployeeViewModel> ListAllPaging(int pageIndex, int pageSize, ref int totalRow);

        Employee ViewDetail(int id);

        int Update(Employee obj);

        int Delete(int id);

        bool ChangeStatus(int id);

        int CountByRole(int roleId);
    }

    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {
        public EmployeeRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public bool ChangeStatus(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure("Employee_ChangeStatus", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountByRole(int roleId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@RoleId", roleId);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure("Employee_CountByRole", p);

                return p.Get<int>("Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Create(Employee obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@CreatedDate", obj.CreatedDate);
                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);
                p.Add("@FullName", obj.FullName);
                p.Add("@RoleId", obj.RoleId);
                p.Add("@IsActive", obj.IsActive);

                ExecuteStoredProcedure("Employee_Create", p);
                return p.Get<int>("@Id");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                return DbConnect.Execute("Employee_Delete", p, transaction: Transaction, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EmployeeViewModel> ListAllPaging(int pageIndex, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<EmployeeViewModel>("Employee_ListAllPaging", p);
                totalRow = p.Get<int>("@totalRow");

                return rs.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Employee Login(LoginViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);

                var rs = ExecuteStoredProcedure<Employee>("Login", p);

                return rs.SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(Employee obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);
                p.Add("@FullName", obj.FullName);
                p.Add("@RoleId", obj.RoleId);
                p.Add("@IsActive", obj.IsActive);

                ExecuteStoredProcedure("Employee_Create", p);

                return p.Get<int>("@Id");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Employee ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var res = ExecuteStoredProcedure<Employee>("Employee_ViewDetail", p);

                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
