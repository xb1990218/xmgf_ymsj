using Entities;
using Entities.Models;
using Entities.ViewModels;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Service
{
    public class CommonService: ICommonService
    {
        private EFContext db;

        public CommonService(EFContext eFContext)
        {
            db = eFContext;
        }

        public List<InfoShow> GetInfoShow(int page, int limit,User u,string bedate,out int totalCount)
        {
            List<InfoShow> list = null;
            if (!string.IsNullOrWhiteSpace(bedate))//当日期不为空  也就是说前端搜索填了日期，那要过滤下日期
            {
                var arryDate=bedate.Split(" - ");
                DateTime bgDate = Convert.ToDateTime(arryDate[0]);//开始日期
                DateTime edDate = Convert.ToDateTime(arryDate[1]);//结束日期
                //判断用户是否为管理员，管理员直接连表查出所有数据，普通用户只连表查出他自己的数据
                if (u.IsAdmin == 1)//管理员
                {
                    totalCount = db.User.Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Where(a => a.AddTime >= bgDate && a.AddTime <= edDate).Count();

                    list = db.User.Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Where(a=>a.AddTime>=bgDate&&a.AddTime<=edDate).Skip((page - 1) * limit).Take(limit).ToList();
                }
                else//普通账号
                {
                    totalCount = db.User.Where(a => a.Id == u.Id).Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Where(a => a.AddTime >= bgDate && a.AddTime <= edDate).Count();

                    list = db.User.Where(a => a.Id == u.Id).Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Where(a => a.AddTime >= bgDate && a.AddTime <= edDate).Skip((page - 1) * limit).Take(limit).ToList();
                }
            }
            else//过滤日期为空 则不过滤
            {
                //判断用户是否为管理员，管理员直接连表查出所有数据，普通用户只连表查出他自己的数据
                if (u.IsAdmin == 1)//管理员
                {
                    totalCount = db.User.Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).Count();

                    list = db.User.Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Skip((page - 1) * limit).Take(limit).ToList();
                }
                else//普通账号
                {
                    totalCount = db.User.Where(a => a.Id == u.Id).Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).Count();

                    list = db.User.Where(a => a.Id == u.Id).Join(db.UserInfo, user => user.UserName, fo => fo.Account, (user, fo) => new InfoShow
                    {
                        Id = fo.Id,
                        AddTime = fo.AddTime,
                        AccountName = user.Name,
                        Account = user.UserName,
                        QuDao = fo.QuDao,
                        TGid = fo.TGid,
                        Houz = fo.Houz,
                        UserName = fo.UserName,
                        Mobile = fo.Mobile,
                        Wx = fo.Wx,
                        System = fo.System,
                        Ip = fo.Ip
                    }).OrderByDescending(a => a.AddTime).Skip((page - 1) * limit).Take(limit).ToList();
                }
            }

            return list;
        }
    }
}
