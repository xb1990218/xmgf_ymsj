using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    //主页展示数据模型
    public class InfoShow
    {
        /// <summary>
        /// UserInfo表的主键id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 数据添加日期
        /// </summary>
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 后台登录账号对应的人名Name
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 后台登录用户的账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string QuDao { get; set; }
        /// <summary>
        /// 推广ID
        /// </summary>
        public string TGid { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        public string Houz { get; set; }
        /// <summary>
        /// 用户名-前端页面用户填的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 前端页面用户填的手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 前端页面用户填的微信号
        /// </summary>
        public string Wx { get; set; }
        /// <summary>
        /// 系统：安卓、苹果、电脑
        /// </summary>
        public string System { get; set; }
        /// <summary>
        /// Ip地址-转换成地理位置的
        /// </summary>
        public string Ip { get; set; }
    }
}
