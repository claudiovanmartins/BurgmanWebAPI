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
using System.Web.Http.Description;

namespace SacMobileBurgman.Controllers
{

    public class SalesOrderController : BaseController
    {
        #region Declare
        const string CUSTOMER_SUCCESS_MESSAGE = "Pedido salvo com sucesso!";
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public SalesOrderController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }


        /// <summary>
        /// GetSalesOrder - Get Sales Order by Trade
        /// </summary>
        /// <param name="tradeID"></param>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetSalesOrder(int tradeID, DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                SalesOrderModel model = new SalesOrderModel();
                List<SalesOrderVOResponse> result = model.GetSalesOrder(tradeID, updatedAt);

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

        /// <summary>
        /// PostSalesOrder - Post SalesOrder to Server
        /// </summary>
        /// <param name="salesOrder"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostSalesOrder([FromBody] SalesOrderVORequest salesOrder)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();
                
                SalesOrderModel model = new SalesOrderModel();
                int result = model.PostSalesOrder(salesOrder);

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

        #endregion

    }
}
