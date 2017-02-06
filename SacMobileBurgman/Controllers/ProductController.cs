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
    public class ProductController : BaseController
    {
        #region Declare
        const string PRODUCT_SUCCESS_MESSAGE = "Produto listado com sucesso!";
        #endregion

        #region Public Methods

        public ProductController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// GetProduct - Get list of Products by Date
        /// </summary>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetProduct(DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                ProductModel model = new ProductModel();
                List<ProductVO> result = model.GetProduct(updatedAt);

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
        /// GetProductPrice - Get list of price of the products 
        /// </summary>
        /// <param name="tablePriceID"></param>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetProductPrice(int tradeID, DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                ProductModel model = new ProductModel();
                List<ProductPriceVO> result = model.GetProductPrice(tradeID, updatedAt);

                if (result.Count > 0)
                {
                    response.Response = result;
                    responseCode = HttpStatusCode.OK;
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

        /// <summary>
        /// GetTablePrice - Get Table Prices
        /// </summary>
        /// <param name="tradeID"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetTablePrice(int tradeID)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                ProductModel model = new ProductModel();
                List<TablePriceVOResponse> result = model.GetTablePrice(tradeID);

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
