using SacMobileBurgman.Common;
using SacMobileBurgman.Models;
using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.Models.ModelsResponse;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class SyncController : BaseController
    {
        #region Declare

        #endregion

        #region Public Methods

        public SyncController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// GetSyncItems - Get News Itens to Synchronize 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetSyncItems(int UserID, DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                SyncModel model = new SyncModel();
                List<SyncVOResponse> result = model.GetSyncItems(UserID, updatedAt);

                if (result.Count > 0)
                {
                    response.Response = result;
                    responseCode = HttpStatusCode.OK;
                }
            }
            catch
            {
                HttpResponseMessage response =
                    this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, HttpStatusCodeEnum.BadRequest.ToDescription());
                throw new HttpResponseException(response);
            }
            return Request.CreateResponse(responseCode, response, formatter);
        }

        #endregion


        
    }
}
