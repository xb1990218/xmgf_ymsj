using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Services.IService;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        //公共服务
        private ICommonService comSice;
        //缓存服务
        private IMemoryCache cache;
        //UserInfo表服务
        private IUserInfoService userInfoSice;
        //登录用户表服务
        private IUserService userSice;
        public HomeController(ICommonService commonService,IMemoryCache memoryCache, IUserService userService, IUserInfoService _userInfoSice)
        {
            comSice = commonService;
            cache = memoryCache;
            userSice = userService;
            userInfoSice = _userInfoSice;
        }
        //首页
        public IActionResult Index()
        {
            int? id=HttpContext.Session.GetInt32("userId");
            User u = null;
            if (!cache.TryGetValue<User>($"userid_{id}",out  u))
            {
                u=userSice.GetEntity(a => a.Id == id);
                //设置到缓存中 过期时间一小时
                cache.Set<User>($"userid_{u.Id}", u, DateTimeOffset.Now.AddHours(1));
            }
            ViewBag.user = u;
            return View();
        }

        //信息查询页面
        public IActionResult List()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            User u = null;
            if (!cache.TryGetValue<User>($"userid_{id}", out u))
            {
                u = userSice.GetEntity(a => a.Id == id);
                //设置到缓存中 过期时间一小时
                cache.Set<User>($"userid_{u.Id}", u, DateTimeOffset.Now.AddHours(1));
            }
            ViewBag.user = u;
            return View();
        }

        /// <summary>
        /// 信息查询页面分页获取数据方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="id">登录用户的id</param>
        /// <returns></returns>
        public JsonResult GetData(int page,int limit,int id,string bedate)
        {
            //先判断该用户是管理员还是普通用户，管理员直接查出所有数据，普通用户只查出他自己的数据
            User u = null;
            if (!cache.TryGetValue<User>($"userid_{id}", out u))
            {
                u = userSice.GetEntity(a => a.Id == id);
                //设置到缓存中 过期时间一小时
                cache.Set<User>($"userid_{u.Id}", u, DateTimeOffset.Now.AddHours(1));
            }
            List<InfoShow> list=comSice.GetInfoShow(page, limit, u,bedate, out int totalCount);
            return Json(new { code=0, msg="", count= totalCount, data=list });
        }

        //渠道商账号汇总查询页面分页获取数据的方法
        public JsonResult GetBusinessData(int page, int limit, string bedate)
        {
            //获取当前登录用户
            int? id = HttpContext.Session.GetInt32("userId");
            User u = null;
            if (!cache.TryGetValue<User>($"userid_{id}", out u))
            {
                u = userSice.GetEntity(a => a.Id == id);
                //设置到缓存中 过期时间一小时
                cache.Set<User>($"userid_{u.Id}", u, DateTimeOffset.Now.AddHours(1));
            }
            //获取该渠道商下面的客户用户(约定一个客户账号只有一个渠道商账号 一对一关系)
            //User nUser = userSice.GetEntity(a => a.ParentId == u.Id);
            //对该nUser账号对应的UserInfo表信息按照后缀分组得总数 再分页
            List<BusineShow> list= comSice.GetBusInfoShow(page, limit, u,bedate, out int totalCount);
            return Json(new { code = 0, msg = "", count = totalCount, data = list });
        }

        //渠道商账号的汇总查询页面
        public IActionResult BusinessList()
        {
            return View();
        }

        //修改密码页面
        public IActionResult Password()
        {
            return View();
        }

        //用户列表
        public IActionResult UserList()
        {
            return View();
        }

        public JsonResult UpdatePassword(string oldpassword,string newpassword)
        {
            int? id = HttpContext.Session.GetInt32("userId");
            User u = null;
            if (!cache.TryGetValue<User>($"userid_{id}", out u))
            {
                u = userSice.GetEntity(a => a.Id == id);
                //设置到缓存中 过期时间一小时
                cache.Set<User>($"userid_{u.Id}", u, DateTimeOffset.Now.AddHours(1));
            }
            //判断原密码是否正确
            if (u.Pasword!=oldpassword)
            {
                return Json(new BoolResult { Result=false,Msg="原密码错误！"});
            }
            //更新密码
            u.Pasword = newpassword;
            bool b=userSice.Edit(u);
            if (b)
            {
                HttpContext.Session.Remove("userId");
                return Json(new BoolResult { Result = true, Msg = "密码更新成功！" });
            }
            else
            {
                return Json(new BoolResult { Result = false, Msg = "网络异常，请重试！" });
            }
        }
    }
}