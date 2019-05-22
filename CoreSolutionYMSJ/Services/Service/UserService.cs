using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    public class UserService:BaseService<User>,IUserService
    {
        public UserService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
