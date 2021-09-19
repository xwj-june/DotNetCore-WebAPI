using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Filters
{
    public class Version1DiscontinueResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
            {
                //to stop it from there, we will return a result directly
                context.Result = new BadRequestObjectResult(new
                {
                    Versioning = new[] { "This version of the API has expried, please use the latest." }
                });
            }
        }
    }
}
