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
    public interface INewsRepository
    {
        bool Create(News obj);

        bool Edit(News obj);

        bool ChangePublished(int id);

        IEnumerable<News> ListAll(int top = 0);

        IEnumerable<News> News_ListAllPaging(int page, int pageSize, ref int totalRow, string keyword = "", bool exclude = false);

        News ViewDetail(int id);
    }

    public class NewsRepository : RepositoryBase, INewsRepository
    {
        public NewsRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public bool ChangePublished(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure("News_ChangePublished", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(News obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@CreatedDate", obj.CreatedDate);
                p.Add("@Type", obj.Type);
                p.Add("@Title", obj.Title);
                p.Add("@Detail", obj.Detail);
                p.Add("@Published", obj.Published);
                p.Add("@Avatar", obj.Avatar);
                p.Add("@Description", obj.Description);

                var rs = ExecuteStoredProcedure("News_Create", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<News> ListAll(int top = 0)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Top", top);

                var rs = ExecuteStoredProcedure<News>("News_ListAll", p);

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<News> News_ListAllPaging(int page, int pageSize, ref int totalRow, string keyword = "", bool exclude = false)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Exclude", exclude);
                p.Add("@Keyword", keyword);
                p.Add("@pageIndex", page);
                p.Add("@pageSize", pageSize);
                p.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var rs = ExecuteStoredProcedure<News>("News_ListAll_Paging", p);

                totalRow = p.Get<int>("totalRow");

                return rs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Edit(News obj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", obj.Id);
                p.Add("@CreatedDate", obj.CreatedDate);
                p.Add("@Type", obj.Type);
                p.Add("@Title", obj.Title);
                p.Add("@Detail", obj.Detail);
                p.Add("@Published", obj.Published);
                p.Add("@Avatar", obj.Avatar);
                p.Add("@Description", obj.Description);

                var rs = ExecuteStoredProcedure("News_Edit", p);

                return rs > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public News ViewDetail(int id)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@Id", id);

                var rs = ExecuteStoredProcedure<News>("News_ViewDetail", p);

                return rs.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
