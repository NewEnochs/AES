using System;
using SqlSugar;

namespace AES.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("SYS_USER")]
    public partial class SYS_USER : DEntityBase
    {
        /// <summary>
        /// 消息通知邮件通知（0：否 1：是）
        /// </summary>
        public int? XXTZYJTZ { get; set; }

        /// <summary>
        /// 消息通知短信通知（0：否 1：是）
        /// </summary>
        public int? XXTZDXTZ { get; set; }

        /// <summary>
        /// 消息通知界面通知（0：否 1：是）
        /// </summary>
        public int? XXTZJMTZ { get; set; }

        /// <summary>
        /// 重点人群引进（0：否 1：是）
        /// </summary>
        public int? ZDRQYJ { get; set; }

        /// <summary>
        /// 用户介绍
        /// </summary>
        public string YHJS { get; set; }

        /// <summary>
        /// 专业技术服务，读取字典
        /// </summary>
        public string ZYJSZW { get; set; }

        /// <summary>
        /// 最高学历编码，读取字典
        /// </summary>
        public string ZGXL { get; set; }

        /// <summary>
        /// 科室GUID
        /// </summary>
        public string SYS_DEPARTMENT_GUID { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int? ORGANID { get; set; }

        /// <summary>
        /// 国际编码，读取字典
        /// </summary>
        public string GJ { get; set; }

        /// <summary>
        /// 民族编码，读取字典
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 用户类别编码，读取字典
        /// </summary>
        public string YHLB { get; set; }

        /// <summary>
        /// 用户类型编码，读取字典
        /// </summary>
        public string YHLX { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string ZJHM { get; set; }

        /// <summary>
        /// 证件类型（1：身份证 2：护照 ）
        /// </summary>
        public int? ZJLX { get; set; }

        /// <summary>
        /// 登录失败次数（默认0）
        /// </summary>
        public int DLSBCS { get; set; }

        /// <summary>
        /// 主题色
        /// </summary>
        public string ZTS { get; set; }

        /// <summary>
        /// 隐藏菜单栏（0：否 1：是）
        /// </summary>
        public int YCCDL { get; set; }

        /// <summary>
        /// 管理员类型-超级管理员_1、非管理员_2
        /// </summary>
        public int AdminType { get; set; }

        /// <summary>
        /// 状态-正常_0、停用_1、删除_2
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别（0：女 1：男）
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BIRTHDAY { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 是否正在登录（0：否 1：是）
        /// </summary>
        public int SFDL { get; set; }
        /// <summary>
        /// IM在线状态（0：下线 1：在线）
        /// </summary>
        public int IMZXZT { get; set; }

    }
}
