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
    public interface ICustomerRepository
    {
        int Register(Customer obj);
        int Login(LoginViewModel obj);
        IEnumerable<CustomerViewModel> ListCustomerPaging(string fullName, string email, int page, int pageSize, ref int totalRow);
        int ChangeStatus(int Id);
        CustomerViewModel ViewDetail(int Id);
        CustomerViewModel ViewDetail(string email);
        int Update(CustomerViewModel obj);
        bool ContactUs_Insert(ContactUs obj);
        int ChangePassword(ChangePasswordViewModel obj);

    }
 
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int ChangePassword(ChangePasswordViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);
                p.Add("@NewPassword", obj.NewPassword);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Customer_ChangePassword", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public int ChangeStatus(int Id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", Id);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Customer_ChangePassword", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ContactUs_Insert(ContactUs obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Title", obj.Title);
                p.Add("@Content", obj.Content);
                p.Add("@FullName", obj.FullName);
                p.Add("@Email", obj.Email);
                p.Add("@CreatedDate", obj.CreatedDate);
                p.Add("@Status", obj.Status);

                var rs = ExecuteStoredProcedure("ContactUs_Insert", p);

                return rs <= 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerViewModel> ListCustomerPaging(string fullName, string email, int page, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@FullName", fullName);
                p.Add("@Email", email);
                p.Add("@PageIndex", page);
                p.Add("@PageSize", pageSize);
                p.Add("@TotalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<CustomerViewModel>("CustomerFullInfo_ListAll", p);

                totalRow = p.Get<int>("@TotalRow");

                return rs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Login(LoginViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Customer_Login", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Register(Customer obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@GuidId", obj.GuidId.ToString());
                p.Add("@Status", obj.Status);
                p.Add("@CreatedDate",obj.CreatedDate);
                p.Add("@Email", obj.Email);
                p.Add("@Password", obj.Password);

                ExecuteStoredProcedure("Customer_Create", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(CustomerViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", obj.Id);
                p.Add("@Email", obj.Email);
                p.Add("@GuidId", obj.GuidId);
                p.Add("@PhoneNo", obj.PhoneNo);
                p.Add("@FullName", obj.FullName);
                p.Add("@Address", obj.Address);
                p.Add("@Status", obj.Status);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("CustomerFullInfo_Update", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomerViewModel ViewDetail(int Id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", Id);

                var res = ExecuteStoredProcedure<CustomerViewModel>("CustomerFullInfo_ViewDetail", p);

                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomerViewModel ViewDetail(string email)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Email", email);

                var res = ExecuteStoredProcedure<CustomerViewModel>("CustomerFullInfo_ViewDetail_ByEmail", p);

                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
