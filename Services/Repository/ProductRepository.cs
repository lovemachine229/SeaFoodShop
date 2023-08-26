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
    public interface IProductRepository
    {
        IEnumerable<Product> ListAllProduct(int top = 0);

        IEnumerable<Product> ListProductPaging(string keyword, string code, int page, int pageSize, ref int totalRow, decimal minPrice = 0, decimal maxPrice = 0);

        IEnumerable<Product> HotProducts(int top = 0);

        IEnumerable<Product> ListBestDeal(int top = 0);

        IEnumerable<Product> ListBestSeller(int top = 0);

        IEnumerable<ProductViewModel> Statistic_Product(string type, int page, int pageSize, ref int totalRow);

        IEnumerable<Product> ListBestDeal_Paging(int page, int pageSize, ref int totalRow);

        IEnumerable<Product> ListBestSeller_Paging(int page, int pageSize, ref int totalRow);

        long Create(ProductViewModel obj);

        int ProductCate_Create(ProductCate obj);

        ProductViewModel ViewDetail(long id);

        int CountByCate(int id);

        int Update(ProductViewModel obj);

        int ProductCate_DeleteByProductId(long id);

        bool ChangeStatus(int id);

        bool ChangeIsHot(int id);

        IEnumerable<Product> BestDeals(int top = 0);

        IEnumerable<Product> RelatedProducts(int cateId, int top = 0);

        bool Get_Price_Range(ref decimal MinValue, ref decimal MaxValue);

        bool UpdateQuantity(long id, int quantity);

        List<Apriori.Model.Product> GetAprioriSupports();
    }

    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public IEnumerable<Product> BestDeals(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);

                var rs = ExecuteStoredProcedure<Product>("Product_BestDeals", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ChangeIsHot(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure("Product_ChangeIsHot", p);

                return rs > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ChangeStatus(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure("Product_ChangeStatus", p);

                return rs > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CountByCate(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure("Product_CountByCate", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public long Create(ProductViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", obj.Id);
                p.Add("@View", obj.View);
                p.Add("@Name", obj.Name);
                p.Add("@Code", obj.Code);
                p.Add("@Detail", obj.Detail);
                p.Add("@Description", obj.Description);
                p.Add("@Avatar", obj.Avatar);
                p.Add("@Images", obj.Images);
                p.Add("@Price", obj.Price);
                p.Add("@UnitPrice", obj.UnitPrice);
                p.Add("@SaleOff", obj.SaleOff);
                p.Add("@StartDate", obj.StartDate);
                p.Add("@EndDate", obj.EndDate);
                p.Add("@Published", obj.Published);
                p.Add("@IsHot", obj.IsHot);
                p.Add("@Quantity", obj.Qualtity);
                p.Add("@Unit", obj.Unit);

                ExecuteStoredProcedure("Product_Create", p);

                return p.Get<long>("@Id");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Apriori.Model.Product> GetAprioriSupports()
        {
            try
            {
                var p = new DynamicParameters();

                var rs = ExecuteStoredProcedure<Apriori.Model.Product>("Proc_Apriori_GetSupports", p);

                return rs.AsList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Get_Price_Range(ref decimal MinValue, ref decimal MaxValue)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@MinValue", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                p.Add("@MaxValue", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure("Price_Range", p);

                MinValue = p.Get<decimal>("@MinValue");
                MinValue = p.Get<decimal>("@MaxValue");

                return rs > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> HotProducts(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);

                var rs = ExecuteStoredProcedure<Product>("Product_ListHot", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListAllProduct(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);

                var rs = ExecuteStoredProcedure<Product>("Product_ListAll", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListBestDeal(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);

                var rs = ExecuteStoredProcedure<Product>("Product_ListDeal", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListBestDeal_Paging(int page, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@pageIndex", page);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<Product>("Product_ListDeal_Paging", p);

                totalRow = p.Get<int>("@totalRow");

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListBestSeller(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);

                var rs = ExecuteStoredProcedure<Product>("Product_ListBestSeller", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListBestSeller_Paging(int page, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@pageIndex", page);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<Product>("Product_ListBestSeller_Paging", p);

                totalRow = p.Get<int>("@totalRow");

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> ListProductPaging(string keyword, string code, int page, int pageSize, ref int totalRow, decimal minPrice = 0, decimal maxPrice = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@MinPrice", minPrice);
                p.Add("@MaxPrice", maxPrice);
                p.Add("@code", code);
                p.Add("@keyword", keyword);
                p.Add("@pageIndex", page);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<Product>("Product_ListAllPaging", p);

                totalRow = p.Get<int>("@totalRow");

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ProductCate_Create(ProductCate obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@ProductId", obj.ProductId);
                p.Add("@CateId", obj.CateId);
                p.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("ProductCate_Create", p);

                return p.Get<int>("@Id");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int ProductCate_DeleteByProductId(long id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@ProductId", id);

                var rs = ExecuteStoredProcedure("ProductCate_DeleteByProductId", p);

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Product> RelatedProducts(int cateId, int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@top", top);
                p.Add("@CateId", cateId);

                return ExecuteStoredProcedure<Product>("Product_RelatedList", p);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<ProductViewModel> Statistic_Product(string type, int page, int pageSize, ref int totalRow)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@type", type);
                p.Add("@pageIndex", page);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<ProductViewModel>("Product_Statistic_Paging", p);
                totalRow = p.Get<int>("@totalRow");

                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(ProductViewModel obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", dbType: DbType.Int64, direction: ParameterDirection.Output);
                p.Add("@Name", obj.Name);
                p.Add("@Code", obj.Code);
                p.Add("@Detail", obj.Detail);
                p.Add("@Description", obj.Description);
                p.Add("@Avatar", obj.Avatar);
                p.Add("@Images", obj.Images);
                p.Add("@Price", obj.Price);
                p.Add("@UnitPrice", obj.UnitPrice);
                p.Add("@SaleOff", obj.SaleOff);
                p.Add("@StartDate", obj.StartDate);
                p.Add("@EndDate", obj.EndDate);
                p.Add("@Published", obj.Published);
                p.Add("@IsHot", obj.IsHot);
                p.Add("@Quantity", obj.Qualtity);
                p.Add("@Unit", obj.Unit);
                p.Add("@Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

                ExecuteStoredProcedure("Product_Update", p);

                return p.Get<int>("@Output");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateQuantity(long id, int quantity)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);
                p.Add("@Quantity", quantity);

                var rs = ExecuteStoredProcedure("Product_UpdateQuantity", p);

                return rs > 0 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProductViewModel ViewDetail(long id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure<ProductViewModel>("Product_ViewDetal", p);

                return rs.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
