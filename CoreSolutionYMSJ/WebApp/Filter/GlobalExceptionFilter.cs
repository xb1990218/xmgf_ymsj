using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filter
{
    /// <summary>
    /// 过滤器-全局异常处理
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)//如果异常没处理
            {
                //判断请求是否来源ajax
                if (context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                {
                    context.ExceptionHandled = true;//设置异常已被处理
                    context.Result = new JsonResult(new BoolResult { Result = false, Msg = context.Exception.Message });
                }
                else
                {
                    context.ExceptionHandled = true;//设置异常已被处理
                    string msg = context.Exception.Message;
                    context.Result = new RedirectResult("/Account/Error");
                }
            }
        }
    }
}
