using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services.IService;

namespace WebApp.Controllers
{
    public class ApiController : Controller
    {
        //公共服务
        private ICommonService comSice;
        //缓存服务
        private IMemoryCache cache;
        //UserInfo表服务
        private IUserInfoService userInfoSice;
        //登录用户表服务
        private IUserService userSice;
        public ApiController(ICommonService commonService, IMemoryCache memoryCache, IUserService userService, IUserInfoService _userInfoSice)
        {
            comSice = commonService;
            cache = memoryCache;
            userSice = userService;
            userInfoSice = _userInfoSice;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(string account = "", string qudao = "", string tuigid = "", string houzhui = "", string username = "", string mobile = "", string wx = "", string system = "", string ip = "")
        {
            User u = userSice.GetEntity(a => a.UserName == account);
            //首先要保证account账户已经存在，也就是说后台开了这个账号，否则插入数据无意义
            if (u == null)
            {
                return Json(new BoolResult { Result = false, Msg = "不正确的account参数，请检查" });
            }
            //渠道 推广id 后缀都不能为空-这三个是代表信息归属的
            if (qudao == "" || tuigid == "" || houzhui == "")
            {
                return Json(new BoolResult { Result = false, Msg = "qudao，tuigid，houzhui这三个参数不能为空" });
            }
            //如果要收集的客户信息都为空，则也是没有意义的数据
            if (username == "" && mobile == "" && wx == "" && system == "" && ip == "")
            {
                return Json(new BoolResult { Result = false, Msg = "用户信息不能都为空" });
            }
            UserInfo ufo = new UserInfo();
            ufo.Account = account;
            ufo.QuDao = qudao;
            ufo.TGid = tuigid;
            ufo.Houz = houzhui;
            ufo.UserName = username;
            ufo.Mobile = mobile;
            ufo.Wx = wx;
            ufo.System = system;
            ufo.Ip = ip;
            ufo.AddTime = DateTime.Now;
            userInfoSice.Add(ufo);
            return Json(new BoolResult { Result = true, Msg = "成功" });
        }
    }
}