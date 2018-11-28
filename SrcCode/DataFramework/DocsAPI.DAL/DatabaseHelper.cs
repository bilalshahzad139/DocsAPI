using System;
using System.Configuration;

namespace DocsAPI.DAL
{    
    internal static class DatabaseHelper
    {
        private static readonly object SyncRoot = new Object();
        private static readonly Boolean IsCSEncrypted = false;
        public static string MainDBConnectionString
        {
            get;
            private set;
        }
       
        static DatabaseHelper()
        {
            String connStr = ConfigurationManager.ConnectionStrings["MainDBConnectionString"].ConnectionString;

            bool flag = false;
            Boolean.TryParse(ConfigurationManager.AppSettings["IsCSEncrypted"], out flag);
            IsCSEncrypted = flag;

            if (IsCSEncrypted)
            {
                connStr = PUCIT.AIMRL.Common.EncryptDecryptUtility.Decrypt(connStr);
            }
            MainDBConnectionString = connStr;
        }
                
        
    }
}