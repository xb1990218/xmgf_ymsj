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
            //验证用户名和密码
            User u=userSice.GetEntity(a => a.UserName == username && a.Pasword == password);
            if (u==null)
            {
                return Json(new BoolResult {Result=false,Msg="用户名或密码错误！" });
            }

            ////验证通过后写入cookies
            ////写入Cookies,写入数据格式为：id|随机数
            //HttpContext.Response.Cookies.Append("user", u.Id+"|"+u.RndNum, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddMinutes(180)//过期时间120分钟-2小时
            //});
            //return Json(new BoolResult {Result=true,Msg="登录成功！" });

            ////生产一个Guid作为token
            //string token = Guid.NewGuid().ToString();
            ////把token存入到cookies，过期时间两小时,主要是做全局登录验证，每2.5小时后台要重新登录
            //HttpContext.Response.Cookies.Append("user", token, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddHours(3)
            //});
            ////用token作为键，用户实体作为值写入缓存 过期时间2小时
            //cache.Set<User>(token, u, DateTimeOffset.Now.AddHours(3));

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