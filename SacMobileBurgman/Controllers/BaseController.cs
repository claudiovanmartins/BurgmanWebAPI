using SacMobileBurgman.Common;
using SacMobileBurgman.Models.ModelsResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace SacMobileBurgman.Controllers
{
    public class BaseController : ApiController
    {
        #region Declare

        public BaseResponse response;
        public JsonMediaTypeFormatter formatter;
        public HttpStatusCode responseCode;

        #endregion
    }
}
