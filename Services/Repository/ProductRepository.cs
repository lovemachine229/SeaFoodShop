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
    }
    
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
