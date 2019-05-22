using CommonLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Filter
{
    /// <summary>
    /// 过滤器-验证用户是否登录，未登录则跳转到登录页面
    /// </summary>
    public class ValidateLoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ////读取cookies，检查是否存了用户id
            //filterContext.HttpContext.Request.Cookies.TryGetValue("user", out string value);
            //if (string.IsNullOrWhiteSpace(value))//没有则跳转登录页面
            //{
            //    //跳转到登录页面，并带上返回路径以便登陆后跳回去
            //    filterContext.Result = new RedirectResult("/Account/Login");
            //}
            int? uid=filterContext.HttpContext.Session.GetInt32("userId");
            if (uid==null||uid<=0)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
