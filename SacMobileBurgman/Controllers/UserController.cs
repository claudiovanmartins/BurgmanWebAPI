using Newtonsoft.Json;
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
using System.Web;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class UserController : BaseController
    {
        #region Declare

        const string USER_NOT_VALID_MESSAGE = "Usuário ou Senha inválidos";

        #endregion

        #region Public Methods

        public UserController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// PostAccessAuthenticate - Get Access Authenticate
        /// </summary>
        /// <param name="UserVORequest"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostAccessAuthenticate([FromBody] UserVORequest user)
        {
            try
            {
                UserVOResponse result = new UserVOResponse();
                UserModel model = new UserModel();

                response.Response = HttpStatusCodeEnum.Unauthorized.ToDescription();
                responseCode = HttpStatusCode.Unauthorized;

                if (user != null)
                {
                    result = model.GetLoginAuthenticated(user.Login, user.Password);

                    if (result.ID != 0)
                    {
                        model.GetAccessToken(ref result);

                        response.Response = result;
                        responseCode = HttpStatusCode.OK;
                    }
                }
            }
            catch (Exception)
            {
                HttpResponseMessage response =
                    this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, HttpStatusCodeEnum.BadRequest.ToDescription());
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(responseCode, response, formatter);
        }


        /// <summary>
        /// GetLoginAuthenticated - Authentic Login
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetLoginAuthenticated(string login, string password)
        {
            UserVOResponse result = new UserVOResponse();
            UserModel model = new UserModel();

            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                result = model.GetLoginAuthenticated(login, password);

                if (result.ID != 0)
                {
                    model.GetAccessToken(ref result);

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