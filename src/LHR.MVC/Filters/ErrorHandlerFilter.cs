using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Filters;

namespace LHR.MVC.Filters
{
    public class ErrorHandlerFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext context)
        {
            string errorMessage = $"Error occured: {context.Exception.Message + " " + context.Exception.StackTrace}";
            context.HttpContext.Response.Body.Write(System.Text.UnicodeEncoding.Unicode.GetBytes(errorMessage),0,errorMessage.Length);
            //throw new NotImplementedException();
        }
    }
}
