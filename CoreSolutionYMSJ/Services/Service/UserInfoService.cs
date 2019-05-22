using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    public class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        public UserInfoService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
