using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }
        public string RndNum { get; set; }
        public int IsAdmin { get; set; }
        public int IsLock { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 是否是渠道商账号 0否 1是
        /// </summary>
        public int IsBusiness { get; set; }
        /// <summary>
        /// 所属渠道商id  0表示不属于哪个渠道商账号 具体的数字表示所属的渠道商id
        /// </summary>
        public int ParentId { get; set; }
    }
}
