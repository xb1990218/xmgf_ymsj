using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filter;

namespace WebApp.Controllers
{
    /// <summary>
    /// 基控制器，打了过滤器标签，主要用来做登陆验证的，只要继承本控制器 就会进行登陆验证
    /// </summary>
    [ValidateLoginFilter]
    public class BaseController : Controller
    {
        
    }
}