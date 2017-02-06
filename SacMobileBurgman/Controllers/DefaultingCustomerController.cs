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
    public class DefaultingCustomerController : BaseController
    {
        #region Declare
        const string CUSTOMER_SUCCESS_MESSAGE = "Inadimplentes listados com sucesso!";
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public DefaultingCustomerController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// GetDefaultingCustomer - Get Lists of Defaulting Customer
        /// </summary>
        /// <param name="tradeID"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetDefaultingCustomer(int tradeID)
        {
            try
            {
                List<DefaultingCustomerVOResponse> result = new List<DefaultingCustomerVOResponse>();
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                DefaultingCustomerModel model = new DefaultingCustomerModel();
                result = model.GetDefaultingCustomer(tradeID);

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
