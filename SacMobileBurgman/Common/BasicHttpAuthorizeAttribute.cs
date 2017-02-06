using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SacMobileBurgman.Common
{
    public class BasicHttpAuthorizeAttribute : AuthorizeAttribute
    {
        #region Members Variable

        private const string BasicAuthResponseHeader = "WWW-Authenticate";
        private const string BasicAuthResponseHeaderValue = "Basic";
        
        #endregion


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (IsAuthorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var unaUnauthorizedMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            unaUnauthorizedMessage.Headers.Add(BasicAuthResponseHeader, BasicAuthResponseHeaderValue);
            throw new HttpResponseException(unaUnauthorizedMessage);
        }

        private bool IsAuthorize(HttpActionContext actionContext)
        {
            bool result = default(bool);
            AuthenticationHeaderValue auth = actionContext.Request.Headers.Authorization;

            if (string.Compare(auth.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string accessToken = auth.Parameter;
                result = SecurityCommon.ValidateToken(accessToken.ToString());
            }

            return result;
        }
    }
}