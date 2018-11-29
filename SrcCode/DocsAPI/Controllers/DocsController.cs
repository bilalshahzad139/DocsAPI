using DocsAPI.Entities;
using DocsAPI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace DocsAPI.Controllers
{

    public class DocsController : ApiController
    {


        [Authorize]
        [HttpPost]
        public ResponseResult Upload()
        {
            try
            {
                var clientKey = User.Identity.Name;
                var filesList = GetFilesFromRequestAndSave("~/UploadedFiles", clientKey);
                return ResponseResult.GetSuccessObject(filesList);
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                return ResponseResult.GetErrorObject();
            }
        }

        private List<FileDTO> GetFilesFromRequestAndSave(String pFolderPath, String pClientKey)
        {
            List<FileDTO> filesList = new List<FileDTO>();
            try
            {
                foreach (var key in System.Web.HttpContext.Current.Request.Files.AllKeys)
                {
                    System.Web.HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[key];

                    if (file != null)
                    {
                        String extension = "";
                        var uniqueId = SaveFile(file, pFolderPath, out extension);
                        if (!String.IsNullOrEmpty(uniqueId))
                        {
                            var fileDto = new FileDTO();

                            fileDto.ActualFileName = file.FileName;
                            fileDto.ContentType = file.ContentType;
                            fileDto.ConentLengthInBytes = file.ContentLength;
                            fileDto.UniqueName = uniqueId;
                            fileDto.Extension = extension;

                            filesList.Add(fileDto);
                        }
                    }//end of if(file != null)
                }//end of foreach

                if (filesList.Count > 0)
                {
                    filesList = DocsAPI.Models.DocsBAL.SaveFilesData(filesList, pClientKey);
                }

                return filesList;
            }
            catch (Exception ex)
            {
                Util.Utility.HandleException(ex);
                return null;
            }
        }

        private string SaveFile(System.Web.HttpPostedFile file, String baseVirtualPath, out String extension)
        {
            extension = "";
            try
            {
                extension = Path.GetExtension(file.FileName);
                //Generate a unique name using Guid
                var uniqueName = Guid.NewGuid().ToString();
                //Get physical path of our folder where we want to save images
                var rootPath = System.Web.HttpContext.Current.Server.MapPath(baseVirtualPath); //"~/UploadedFilesTemp"

                var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName + extension);
                // Save the uploaded file to "UploadedFiles" folder
                file.SaveAs(fileSavePath);

                return uniqueName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        

        /* Download a file without considering authorization part */
        [HttpGet]
        public HttpResponseMessage Download1(String id)
        {
            try
            {
                //var clientKey = User.Identity.Name;
                var file = DocsAPI.Models.DocsBAL.GetFileByID(id, "");
                return DownloadFile(file);
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        [Authorize]
        [HttpPost]
        public ResponseResult Download(String id)
        {
            try
            {
                var clientKey = User.Identity.Name;
                var file = DocsAPI.Models.DocsBAL.GetFileByID(id, clientKey);
                if(file != null)
                {
                    return ResponseResult.GetSuccessObject(new {
                        FileActualName = file.ActualFileName,
                        FileSize = file.ConentLengthInBytes,
                        FileType = file.ContentType,
                        Extension = file.Extension,
                        UniqueID = file.UniqueName
                    });
                }
                else
                {
                    return ResponseResult.GetErrorObject("Not Found");
                }
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                return ResponseResult.GetErrorObject("Not Found");
            }
        }

        private HttpResponseMessage DownloadFile(FileDTO fileDto)
        {
            try
            {
                var appPhysicalPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles");
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = System.IO.Path.Combine(appPhysicalPath, fileDto.UniqueName + fileDto.Extension);

                byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //String mimeType = MimeType.GetMimeType(file); //You may do your hard coding here based on file extension

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(fileDto.ContentType);
                response.Content.Headers.ContentDisposition.FileName = fileDto.ActualFileName;
                return response;
            }
            catch (Exception ex)
            {
                Utility.HandleException(ex);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

        #region ForTesting 
        [HttpGet]
        public int GetData1()
        {
            return 10;
        }

        [Authorize]
        [HttpGet]
        public int GetData2(int a, int b) //Get data from query string
        {
            return a + b;
        }

        [Authorize]
        [HttpPost]
        public String Save(StudentDTO dto) //Get data from body
        {
            return dto.name + "-testing";
        }

        #endregion
    }

    public class StudentDTO
    {
        public int id { get; set; }
        public String name { get; set; }
    }
}