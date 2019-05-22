using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    /// <summary>
    /// 网站设置实体类-对应配置文件的节点AppSet
    /// </summary>
    public class AppSet
    {
        /// <summary>
        /// 微信公众号Appid
        /// </summary>
        public string WxAppid { get; set; }
        /// <summary>
        /// 微信公众号secret
        /// </summary>
        public string Wxsecret { get; set; }
    }
}
