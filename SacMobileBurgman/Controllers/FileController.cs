using SacMobileBurgman.Common;
using SacMobileBurgman.Models;
using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.Models.ModelsResponse;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class FileController : BaseController
    {
        #region Declare
        const string IMAGE_SUCCESS_MESSAGE = "Imagem/dados salvos com sucesso!";
        string FOLDER_PATH = ConfigurationManager.AppSettings["PathUploadFiles"].ToString();
        #endregion

        #region Public Methods

        public FileController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// PostFileData - Save file in database
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostFileData([FromBody]FileVOResquest fileData)
        {
            try
            {
                byte[] attachmentFile = default(byte[]);

                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                if (fileData != null)
                {
                    if (fileData.AttachmentFile.Length > 0)
                    {
                        attachmentFile = Convert.FromBase64String(fileData.AttachmentFile);

                        //string rootFilePath = FOLDER_PATH; // System.Web.HttpContext.Current.Server.MapPath("~/Files/");

                        //if (Directory.Exists(rootFilePath))
                        //{
                        //    Stream stream = new MemoryStream(fileData.AttachmentFile);
                        //    Image image = Image.FromStream(stream);
                        //    string fullPath = Path.Combine(rootFilePath, fileData.NameImage);

                        //    image.Save(fullPath);
                        //}
                    }

                    FileModel model = new FileModel();
                    var result = model.PostFileData(fileData);

                    if (result != null)
                    {
                        response.Response = result;
                        responseCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception e)
            {
                HttpResponseMessage response =
                       this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, HttpStatusCodeEnum.BadRequest.ToDescription());
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(responseCode, response, formatter);
        }

        private bool URLExists(string url)
        {
            bool result = true;

            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = 5000; // miliseconds
                webRequest.Method = "HEAD";

                webRequest.GetResponse();
            }
            catch
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}
