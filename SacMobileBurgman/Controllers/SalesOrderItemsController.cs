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
    public class SalesOrderItemsController : BaseController
    {
        #region Declare
        const string SALESORDER_ITEM_SUCCESS_MESSAGE = "Itens do Pedido salvo com sucesso!";
        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        public SalesOrderItemsController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }


        /// <summary>
        /// GetSalesOrderItems - Get Items from Sales Order by Trade
        /// </summary>
        /// <param name="tradeID"></param>
        /// <param name="updatedAt"></param>
        /// <returns></returns>

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetSalesOrderItems(int tradeID, DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                SalesOrderItemModel model = new SalesOrderItemModel();
                List<SalesOrderItemVOResponse> result = model.GetSalesOrderItem(tradeID, updatedAt);

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

        /// <summary>
        /// PostSalesOrderItem - Post Items from Sales to Server
        /// </summary>
        /// <param name="PostSalesOrderItem"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public HttpResponseMessage PostSalesOrderItem([FromBody] SalesOrderItemVORequest salesOrderItem)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                SalesOrderItemModel model = new SalesOrderItemModel();
                int result = model.PostSalesOrderItem(salesOrderItem);

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
