using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DocsAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            PUCIT.AIMRL.Common.Logger.LogHandler.ConfigureLogger(Server.MapPath("~/logging.config"));
            PUCIT.AIMRL.Common.EncryptDecryptUtility.SetParameters("aBcDeFgHiJKLMnoPQrSTu", "123456787912345891234", "MD5", 50, "1234512345124512", 256);

            Util.Utility.LogData("Testing");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            try
            {
                Util.Utility.LoadGlobalSettings();
            }
            catch (Exception ex)
            {
                //Utility.HandleException(ex);
            }

            try
            {
                //Create UploadedFiles folder if it doesn't exist
                var uploadedFilesPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");

                if (!System.IO.Directory.Exists(uploadedFilesPath))
                {
                    System.IO.Directory.CreateDirectory(uploadedFilesPath);
                }
            }
            catch (Exception ex)
            {
                Util.Utility.HandleException(ex);
            }
        }
    }
}
