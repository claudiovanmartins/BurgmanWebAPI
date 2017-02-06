using SacMobileBurgman.Common;
using SacMobileBurgman.Models;
using SacMobileBurgman.Models.ModelsResponse;
using SacMobileBurgman.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class TransactionTypeController : BaseController
    {
        #region Declare
        const string TRANSACTIONTYPE_SUCCESS_MESSAGE = "Tipo Movimentacao listado com sucesso!";
        #endregion

        #region Public Methods

        public TransactionTypeController()
        {
            response = new BaseResponse();
            formatter = new JsonMediaTypeFormatter();
            responseCode = new HttpStatusCode();
        }

        /// <summary>
        /// GetTransactionType - Get List of Type of Transactions
        /// </summary>
        /// <param name="updatedAt"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetTransactionType(DateTime updatedAt)
        {
            try
            {
                TransactionTypeModel model = new TransactionTypeModel();
                List<TransactionTypeVOResponse> result = model.GetTransactionType(updatedAt);

                responseCode = HttpStatusCode.NotFound;
                response.Response = HttpStatusCodeEnum.NotFound.ToDescription();

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
