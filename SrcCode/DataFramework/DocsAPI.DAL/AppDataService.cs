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

        public static List<FileDTO> SaveFilesData(List<FileDTO> fileList, DateTime pActivityTime, String pClientKey)
        {

            List<FileDTO> fileListUpdated = new List<FileDTO>();
            using (var db = new AppDataContext())
            {
                foreach (var file in fileList)
                {
                    string query = "execute dbo.SaveFileData @UniqueName,@ActualFileName,@ContentType,@ConentLengthInBytes,@Extension,@CreatedOn,@ClientKey";

                    var args = new DbParameter[] {
                        new SqlParameter { ParameterName = "@UniqueName", Value = file.UniqueName},
                        new SqlParameter { ParameterName = "@ActualFileName", Value = file.ActualFileName},
                        new SqlParameter { ParameterName = "@ContentType", Value = file.ContentType},
                        new SqlParameter { ParameterName = "@ConentLengthInBytes", Value = file.ConentLengthInBytes},
                        new SqlParameter { ParameterName = "@Extension", Value = file.Extension},
                        new SqlParameter { ParameterName = "@CreatedOn", Value = pActivityTime},
                        new SqlParameter { ParameterName = "@ClientKey", Value = pClientKey},
                    };

                    try
                    {
                        db.Database.ExecuteSqlCommand(query, args);
                        fileListUpdated.Add(file);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }//end of using

            return fileListUpdated;
        }

        public static FileDTO GetFileByID(String pUniqueyID, String pClientKey)
        {
            using (var ctx = new AppDataContext())
            {
                string query = "execute dbo.GetFileDataById @pUniqueyID,@pClientKey";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@pUniqueyID", Value = pUniqueyID},
                    new SqlParameter { ParameterName = "@pClientKey", Value = pClientKey}
                };

                var data = ctx.Database.SqlQuery<FileDTO>(query, args).FirstOrDefault();
                return data;
            }
        }
        
    }
}