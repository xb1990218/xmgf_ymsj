using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IService
{
    /// <summary>
    /// 公用接口-主要实现写sql的连表查返回自定义的实体结果，或者自己懒得见service时候可以直接在这里写
    /// </summary>
    public interface ICommonService
    {
         List<InfoShow> GetInfoShow(int page, int limit, User u, string bedate, out int totalCount);
    }
}
