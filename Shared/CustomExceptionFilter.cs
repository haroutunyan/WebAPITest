using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CustomExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)(context.Exception is CustomException e ? e.Code : HttpStatusCode.InternalServerError);
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new { Message = context.Exception.InnerException?.Message ?? context.Exception.Message });
            return Task.CompletedTask;
        }
    }
}
