using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class APIKeyAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeader = "ApiKey";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var clientApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //Dependency injection in Filter
            var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var apiKey = config.GetValue<string>("ApiKey");

            if (apiKey != clientApiKey)
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
