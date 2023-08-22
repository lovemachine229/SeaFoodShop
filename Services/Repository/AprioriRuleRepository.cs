using Dapper;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Services.Repository
{
    public interface IAprioriRuleRepository
    {
        int Save(List<AprioriRule> rules);
        List<Product> GetRecommandProducts(List<string> lstX);
        List<AprioriRule> GetAllRules();
    }

    public class AprioriRuleRepository : RepositoryBase, IAprioriRuleRepository
    {
        public AprioriRuleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public int Save(List<AprioriRule> rules)
        {
            try
            {
                var p = new DynamicParameters();
                XmlSerializer serializer = new XmlSerializer(typeof(List<AprioriRule>));
                var stringWriter = new System.IO.StringWriter();
                serializer.Serialize(stringWriter, rules);
                p.Add("@strRules", stringWriter.ToString());
                return DbConnect.Execute("Proc_Apriori_SaveRules", p, transaction: Transaction, commandType: CommandType.StoredProcedure);
            } 
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Product> GetRecommandProducts(List<string> lstX)
        {
            var p = new DynamicParameters();
            p.Add("strLstX", string.Join("|", lstX));
            var products = DbConnect.Query<Product>("Proc_Apriori_GetRecommendProducts", p, transaction: Transaction, commandType: CommandType.StoredProcedure);
            return products.ToList();
        }

        public List<AprioriRule> GetAllRules()
        {
            var rules = DbConnect.Query<AprioriRule>("Proc_Apriori_GetAllRules", transaction: Transaction, commandType: CommandType.StoredProcedure);
            return rules.ToList();
        }
    }
}
