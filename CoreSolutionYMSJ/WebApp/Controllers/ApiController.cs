using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        public JsonResult SaveData(string account = "",string busAccount="", string qudao = "", string tuigid = "", string houzhui = "", string username = "", string mobile = "", string wx = "", string system = "", string ip = "")
        {
            //如果要收集的客户信息都为空，则是没有意义的数据
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(mobile) && string.IsNullOrWhiteSpace(wx) && string.IsNullOrWhiteSpace(system) && string.IsNullOrWhiteSpace(ip))
            {
                return Json(new BoolResult { Result = false, Msg = "用户信息不能都为空" });
            }

            //渠道 推广id 后缀都不能为空-这三个是代表信息归属的
            if (string.IsNullOrWhiteSpace(qudao) || string.IsNullOrWhiteSpace(houzhui))
            {
                return Json(new BoolResult { Result = false, Msg = "qudao，houzhui这两个参数不能为空" });
            }

            //首先要保证account账户(客户账户)已经存在，也就是说后台开了这个账号，否则插入数据无意义
            User u = userSice.GetEntity(a => a.UserName == account);
            if (u == null)
            {
                return Json(new BoolResult { Result = false, Msg = "不正确的account参数，请检查" });
            }
            //其次要保证busAccount账户(渠道商账户)已经存在，每一条传过来的信息必须属于一个客户账号 和一个渠道商账号
            User busU = userSice.GetEntity(a => a.UserName == busAccount);
            if (busU == null)
            {
                return Json(new BoolResult { Result = false, Msg = "不正确的busAccount参数，请检查" });
            }
            
            //为了防止提交重复数据，一个手机在一个用户下只插入一次
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                UserInfo uf=userInfoSice.GetEntity(a => a.Mobile == mobile && a.Account==account);
                if (uf!=null)
                {
                    return Json(new BoolResult { Result = false, Msg = "一个电话只能提交一次" });
                }
            }
            qudao = HttpUtility.UrlDecode(qudao);
            if (!string.IsNullOrWhiteSpace(username))
            {
                username = HttpUtility.UrlDecode(username);
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                ip = HttpUtility.UrlDecode(ip);
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
            ufo.BusAccount = busAccount;
            try
            {
                userInfoSice.Add(ufo);
            }
            catch (Exception)
            {
                return Json(new BoolResult { Result = false, Msg = "服务器异常" });
            }
            return Json(new BoolResult { Result = true, Msg = "成功" });
        }
    }
}