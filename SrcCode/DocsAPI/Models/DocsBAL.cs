using DocsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsAPI.Models
{
    public class DocsBAL
    {
        public static ClientDTO ValidateClient(String pClientKey, String pSecretKey)
        {
            try
            {
                return DocsAPI.DAL.AppDataService.ValidateClient(pClientKey, pSecretKey);
            }
            catch (Exception ex)
            {
                DocsAPI.Util.Utility.HandleException(ex);
                return null;
            }
        }
        public static List<FileDTO> SaveFilesData(List<FileDTO> fileList, String pClientKey)
        {
            try
            {
                return DocsAPI.DAL.AppDataService.SaveFilesData(fileList, DateTime.UtcNow, pClientKey);
            }
            catch (Exception ex)
            {
                DocsAPI.Util.Utility.HandleException(ex);
                return null;
            }
        }

        public static FileDTO GetFileByID(String pUniqueyID, String pClientKey)
        {
            try
            {
                return DocsAPI.DAL.AppDataService.GetFileByID(pUniqueyID, pClientKey);
            }
            catch (Exception ex)
            {
                DocsAPI.Util.Utility.HandleException(ex);
                return null;
            }
        }
    }
}