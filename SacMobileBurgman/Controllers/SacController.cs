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
    public class SacController : BaseController
    {
        #region Declare
        const string SAC_SUCCESS_MESSAGE = "Sac salvo com sucesso!";
        #endregion

        #region Public Methods

        public SacController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// PostSac - Save List of Sac
        /// </summary>
        /// <param name="sacList"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostSac([FromBody] List<SacVORequest> sacList)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                if (sacList != null)
                {
                    SacModel model = new SacModel();
                    model.PostSac(sacList);

                    response.Response = SAC_SUCCESS_MESSAGE;
                    responseCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response =
                     this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, HttpStatusCodeEnum.BadRequest.ToDescription());
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(responseCode, response, formatter);
        }

        /// <summary>
        /// GetSacType - Get List of Type of Sac
        /// </summary>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetSacType(DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                //var accessToken = Request.Headers.Authorization.Parameter;

                SacModel model = new SacModel();
                List<SacTypeVOResponse> result = model.GetSacType(updatedAt);

                if (result.Count > 0)
                {
                    response.Response = result;
                    responseCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
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
