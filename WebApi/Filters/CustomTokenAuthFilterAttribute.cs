using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;

namespace WebApi.Filters
{
    public class CustomTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "ApiKey";
        private const string TokenHeader = "TokenHeader";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(TokenHeader, out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenManger = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;
            if (tokenManger == null && !tokenManger.VerifyToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
