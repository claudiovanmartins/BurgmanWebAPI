using Newtonsoft.Json;
using SacMobileBurgman.Common;
using SacMobileBurgman.Models;
using SacMobileBurgman.Models.ModelsResponse;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Description;

namespace SacMobileBurgman.Controllers
{
    public class InventoryController : BaseController
    {
        #region Declare

        const string INVENTORY_SUCCESS_MESSAGE = "Inventário salvo com sucesso!";

        #endregion


        #region Public Methods

        public InventoryController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// PostInventory - Save Inventory.
        /// </summary>
        /// <param name="InventoryVORequest"></param>
        /// <returns></returns>
        //[BasicHttpAuthorize]
        [AcceptVerbs("POST")]
        //[ResponseType(typeof(InventoryVORequest))]
        //public HttpResponseMessage PostInventory(HttpRequestMessage request)
        public HttpResponseMessage PostInventory([FromBody] InventoryVORequest inventory)
        {
            //response.Response = HttpStatusCodeEnum.BadRequest.ToDescription();
            //responseCode = HttpStatusCode.BadRequest;

            //var jsonResult = request.Content.ReadAsStringAsync().Result;
            //InventoryVORequest inventory = JsonConvert.DeserializeObject<List<InventoryVORequest>>(jsonResult).FirstOrDefault();
            try
            {
                if (inventory != null)
                {
                    InventoryModel model = new InventoryModel();
                    model.PostInventory(inventory);

                    response.Response = INVENTORY_SUCCESS_MESSAGE;
                    responseCode = HttpStatusCode.OK;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601)
                {
                    responseCode = HttpStatusCode.Ambiguous;
                    response.Response = HttpStatusCodeEnum.DuplicateKey.ToDescription();
                }
                else
                {
                    responseCode = HttpStatusCode.Conflict;
                    response.Response = HttpStatusCodeEnum.Conflict.ToDescription();
                }
            }
            catch (Exception)
            {
                responseCode = HttpStatusCode.InternalServerError;
                response.Response = HttpStatusCodeEnum.InternalServerError.ToDescription();
            }

            return Request.CreateResponse(responseCode, response, formatter);
        }

        /// <summary>
        /// GetInventoryType - Get List of Type of Inventory
        /// </summary>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        //[BasicHttpAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetInventoryType(DateTime updatedAt)
        {
            try
            {
                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

                //var accessToken = Request.Headers.Authorization.Parameter;

                InventoryModel model = new InventoryModel();
                List<InventoryTypeVOResponse> result = model.GetInventoryType(updatedAt);

                if (result.Count > 0)
                {
                    response.Response = result;
                    responseCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                responseCode = HttpStatusCode.BadRequest;
                response.Response = HttpStatusCodeEnum.BadRequest.ToDescription();
            }

            return Request.CreateResponse(responseCode, response, formatter);
        }

        #endregion
    }
}
