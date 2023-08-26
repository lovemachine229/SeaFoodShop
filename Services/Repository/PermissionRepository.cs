using Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Xml.Serialization;

namespace Services.Repository
{
    public interface IPermissionRepository
    {
        int Upsert(int roleId, List<Permission> listObj);

        List<Permission> GetPermissions(int roleId);
    }

    public class PermissionRepository : RepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public List<Permission> GetPermissions(int roleId)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@RoleId", roleId);

                return ExecuteStoredProcedure<Permission>("Permission_Get", p).AsList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Upsert(int roleId, List<Permission> listObj)
        {
            try
            {
                var p = new DynamicParameters();

                p.Add("@RoleId", roleId);

                XmlSerializer serializer = new XmlSerializer(typeof(List<Permission>));
                var stringWriter = new System.IO.StringWriter();
                serializer.Serialize(stringWriter, listObj);

                p.Add("@StrPers", stringWriter.ToString());

                return ExecuteStoredProcedure("Permission_Upsert", p);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
