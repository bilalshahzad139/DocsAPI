using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Reflection;
using DocsAPI.Entities;

namespace DocsAPI.DAL
{
    public static class AppDataService
    {
        static AppDataService()
        {
            Database.SetInitializer<AppDataContext>(null);
        }
        public static ClientDTO ValidateClient(String pClientKey,String pSecretKey)
        {
            using (var ctx = new AppDataContext())
            {
                string query = "execute dbo.ValidateClient @clientKey,@secretKey";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@clientKey", Value = pClientKey},
                    new SqlParameter { ParameterName = "@secretKey", Value = pSecretKey}
                };

                var data = ctx.Database.SqlQuery<ClientDTO>(query, args).FirstOrDefault();
                return data;
            }
        }
    }
}