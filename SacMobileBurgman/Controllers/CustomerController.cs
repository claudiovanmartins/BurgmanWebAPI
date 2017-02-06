using SacMobileBurgman.Common;
using SacMobileBurgman.Models;
using SacMobileBurgman.Models.Helper;
using SacMobileBurgman.Models.ModelsResponse;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class CustomerController : BaseController
    {
        #region Declare
        const string CUSTOMER_SUCCESS_MESSAGE = "Empresa salva com sucesso!";
        #endregion

        #region Public Methods

        public CustomerController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }


        /// <summary>
        /// PostCustomer - Post Customer to Server
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostCustomer([FromBody] CustomerVO customer)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                CustomerModel model = new CustomerModel();
                int result = model.PostCustomer(customer);

                if (result > 0)
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

        /// <summary>
        /// GetCustomer - Get Customer from Server
        /// </summary>
        /// <param name="tradeID"></param>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetCustomer(int tradeID, DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                CustomerModel model = new CustomerModel();
                List<CustomerVO> result = model.GetCustomer(tradeID, updatedAt);

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
