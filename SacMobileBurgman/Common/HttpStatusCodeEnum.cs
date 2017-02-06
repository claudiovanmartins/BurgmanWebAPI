using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SacMobileBurgman.Common
{
    public enum HttpStatusCodeEnum
    {
        [Description("OK")]
        OK = 200,

        [Description("Cannot Insert Duplicate Key")]
        DuplicateKey = 208,

        [Description("Conflicted Foreign Key")]
        ForeignKey = 209,

        [Description("Bad Request")]
        BadRequest = 400,

        [Description("Unauthorized")]
        Unauthorized = 401,

        [Description("Forbidden")]
        Forbidden = 403,

        [Description("Not Found")]
        NotFound = 404,

        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,

        [Description("Request Timeout")]
        RequestTimeout = 408,

        [Description("Conflict")]
        Conflict = 409,

        [Description("Internal Server Error")]
        InternalServerError = 500
    }

}