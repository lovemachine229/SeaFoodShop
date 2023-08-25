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
    public interface IOrderRepository
    {
        int Insert(Order obj);

        Order ViewOrder(int id);

        int ChechkOrder(int orderId, string email);

        bool ChangeStatus(int orderId, int statusId);

        bool ChangePayment(int orderId, int payment);

        IEnumerable<ChartsDataViewModel> statiticsByDay(DateTime fromDate, DateTime toDate);

        IEnumerable<Order> ListByCustomer(int customerId);

        IEnumerable<Order> ListAll_Paging(int pageIndex, int pageSize, ref int totalRow, int orderId = 0, string email = "");

        IEnumerable<OrderItem> ViewListOrderItem(int orderId);

        int InsertItem(OrderItem obj);

        int InsertAddressDelivery(AddressDelivery obj);

        AddressDelivery ViewAddressDelivery(int orderId);

        List<Apriori.Model.Order> GetAprioriOrders();
    }

    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public bool ChangePayment(int orderId, int payment)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@orderId", orderId);
                p.Add("@payment", payment);

                var rs = ExecuteStoredProcedure("Order_ChangePayment", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ChangeStatus(int orderId, int statusId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@orderId", orderId);
                p.Add("@statusId", statusId);

                var rs = ExecuteStoredProcedure("Order_ChangeStatus", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ChechkOrder(int orderId, string email)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@OrderId", orderId);
                p.Add("@Email", email);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure("Order_CheckOrder", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Apriori.Model.Order> GetAprioriOrders()
        {
            try
            {
                var p = new DynamicParameters();

                var rs = ExecuteStoredProcedure<Apriori.Model.Order>("Proc_Apriori_GetOrders", p);

                return rs.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Insert(Order obj)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@CustomerId", obj.CustomerId);
                p.Add("@Email", obj.Email);
                p.Add("@Date", obj.Date);
                p.Add("@TotalAmount", obj.TotalAmount);
                p.Add("@Status", obj.Status);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Order_Insert", p);

                return p.Get<int>("@Output");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertAddressDelivery(AddressDelivery obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@OrderId", obj.OrderId);
                p.Add("@FullName", obj.FullName);
                p.Add("@Email", obj.Email);
                p.Add("@PhoneNo", obj.PhoneNo);
                p.Add("@Address", obj.Address);
                p.Add("@Note", obj.Note);

                var rs = ExecuteStoredProcedure("AddressDelivery_Insert", p);

                return rs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int InsertItem(OrderItem obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@OrderId", obj.OrderId);
                p.Add("@ProductId", obj.ProductId);
                p.Add("@Price", obj.Price);
                p.Add("@Quantity", obj.Quantity);
                p.Add("@ProductName", obj.ProductName);
                p.Add("@LastPrice", obj.LastPrice);

                var rs = ExecuteStoredProcedure("OrderItem_Insert", p);

                return rs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Order> ListAll_Paging(int pageIndex, int pageSize, ref int totalRow, int orderId = 0, string email = "")
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@pageIndex", pageIndex);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@orderId", orderId);
                p.Add("@email", email);

                var rs = ExecuteStoredProcedure<Order>("Order_ListAllPaging", p);

                totalRow = p.Get<int>("@totalRow");
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Order> ListByCustomer(int customerId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@CustomerId", customerId);

                var rs = ExecuteStoredProcedure<Order>("Order_ListByCustomer", p);

                return rs;
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
        }

        public IEnumerable<ChartsDataViewModel> statisticsByDay(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@fromDate", fromDate);
                p.Add("@toDate", toDate);

                var rs = ExecuteStoredProcedure<ChartsDataViewModel>("Order_StatisticsByDay", p);

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressDelivery ViewAddressDelivery(int orderId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@OrderId", orderId);

                var rs = ExecuteStoredProcedure<AddressDelivery>("AddressDelivery_ViewDetail", p);

                return rs.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<OrderItem> ViewListOrderItem(int orderId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@OrderId", orderId);

                var rs = ExecuteStoredProcedure<OrderItem>("OrderItem_ListAll", p);

                return rs.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order ViewOrder(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure<Order>("Order_ViewDetail", p);

                return rs.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
