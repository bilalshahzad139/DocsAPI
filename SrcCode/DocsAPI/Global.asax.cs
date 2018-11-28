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
            GlobalConfiguration.Configure(WebApiConfig.Register);

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
