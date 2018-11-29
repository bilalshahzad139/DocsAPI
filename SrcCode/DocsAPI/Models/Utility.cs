using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using PUCIT.AIMRL.Common.Logger;

namespace DocsAPI.Util
{


    public static class Utility
    {

        public static String GetRequestedPageName()
        {
            String pageName = "";

            String[] completeURL = System.Web.HttpContext.Current.Request.ServerVariables["URL"].ToString().Split('/');
            pageName = completeURL[completeURL.Length - 1].ToString();

            return pageName;
        }
        public static String GetUserIPAddress()
        {
            //var ipAddress = "";//HttpContext.Current.Request.Headers["X-Forwarded-For"];
            //if (String.IsNullOrEmpty(ipAddress))
            //var ipAddress = HttpContext.Current.Request.UserHostAddress.ToString();

            return "";
        }

        /// <summary>
        /// Writes a log entry for an exception to file, database and email as specified in configuration
        /// </summary>
        /// <param name="pEx">Exception object</param>
        public static void HandleException(Exception pEx)
        {
            PUCIT.AIMRL.Common.Logger.LogHandler.WriteLog(GetUserNameForLogging(), pEx.Message, PUCIT.AIMRL.Common.Logger.LogType.ErrorMsg, pEx);
        }

        public static void LogData(String pLogEntry)
        {
            PUCIT.AIMRL.Common.Logger.LogHandler.WriteLog(GetUserNameForLogging(), pLogEntry, PUCIT.AIMRL.Common.Logger.LogType.InfoMsg);
        }

        private static String GetUserNameForLogging()
        {
            return "";
            //var userName = "";

            ////if (HttpContext.Current.Request.UserHostAddress != null)
            //userName += "-IP:" + Utility.GetUserIPAddress();

            //if (HttpContext.Current.Request.Url != null && HttpContext.Current.Request.Url.PathAndQuery != null)
            //    userName += "-URL: " + HttpContext.Current.Request.Url.AbsoluteUri;

            //return userName;
        }



        public static void LoadApplicationSettingFromWebConfig()
        {
            Boolean flag = false;
            Boolean.TryParse(ConfigurationManager.AppSettings["IsCSEncrypted"], out flag);

            //GlobalDataManager.IsCSEncrypted = flag;


        }
        public static void LoadGlobalSettings()
        {
            LoadApplicationSettingFromWebConfig();
            //GlobalDataManager.MessagesList = DocsAPI.DAL.AppDataService.GetAllNotificationMessages();
        }


    }


}

