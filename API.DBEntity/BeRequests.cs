using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DBEntity.Model
{
    public class BeRequest
    {
        /// <summary>
        /// RSA加密后字符
        /// </summary>
        public string RSASTR { get; set; }

        /// <summary>
        /// Where值，查询时使用，数组格式，字段名称|参数值
        /// </summary>
        public List<string[]> Wheres { get; set; }


        public BeRequest beRequest { get; set; }

        /// <summary>
        /// Key 查询单条OR 删除数据使用,传输数据的唯一值
        /// </summary>
        public string QueryKey { get; set; }
        /// <summary>
        /// Where值，查询时使用,拼接上的Where条件,And
        /// </summary>
        public string QueryWhere { get; set; }
        /// <summary>
        /// Where值，查询时使用,拼接上的Where条件,Or
        /// </summary>
        public string QueryWhereOr { get; set; }
        /// <summary>
        /// 对象，新增，保存,删除时使用。
        /// </summary>
        public Hashtable Objects { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 是否首次查询
        /// </summary>
        public bool IsRefreshQuery { get; set; }

        /// <summary>
        /// 是否执行分页
        /// </summary>
        public bool isPage { get; set; }


        private int _PageIndex;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {

                if (_PageIndex <= 0)
                {
                    return 0;
                }
                return _PageIndex;
            }
            set { _PageIndex = value; }
        }

        /// <summary>
        /// 是否为导出
        /// </summary>
        public int endPage { get; set; }
        /// <summary>

        /// 排序
        /// </summary>
        public string SqlOrder { get; set; }


        /// 请求工号
        /// </summary>
        public string GH { get; set; } = "5232111001";
        public string MM { get; set; } = "1234ASDF";

        public bool ISREPORT { get; set; } = false;

        public string UUID { get; set; }

        /// <summary>
        /// 请求者姓名
        /// </summary>
        public string XM { get; set; }
        /// <summary>
        /// 请求者机构编码
        /// </summary>
        public string JGBM { get; set; } = "10000111001";
        /// <summary>
        /// 请求者县级编码
        /// </summary>
        public string XJJGBM
        {
            get
            {
                if (JGBM == null)
                {
                    return "";
                }
                if (JGBM.Length >= 6)
                {
                    return JGBM.Substring(0, 6);
                }
                else
                {
                    return JGBM;
                }
            }
            set { }
        }
        /// <summary>
        /// 请求者镇级编码
        /// </summary>
        public string ZJJGBM
        {
            get
            {
                if (JGBM == null)
                {
                    return "";
                }
                if (JGBM.Length >= 8)
                {
                    return JGBM.Substring(0, 8);
                }
                else
                {
                    return JGBM;
                }
            }
            set { }
        }
        /// <summary>
        /// 选择机构编码
        /// </summary>
        public string CurryJGBM { get; set; }
        /// <summary>
        /// 请求者机构名称
        /// </summary>
        public string JGMC { get; set; }

        ///// <summary>
        ///// 所属行政单位名称
        ///// </summary>
        //public string SSXZDWMC { get; set; }

        ///// <summary>
        ///// 所属行政单位编码
        ///// </summary>
        //public string SSXZDWBM { get; set; }
        /// <summary>
        /// 登录session
        /// </summary>
        public string LoginGuid { get; set; }

        /// <summary>
        /// 是否允许修改下级机构数据
        /// </summary>
        public bool bEdit { get; set; }

        /// <summary>
        /// 健康教育管理员 0:否 1：是
        /// </summary>
        public int ISJKJYGLY { get; set; }

        /// <summary>
        /// 查询是否带机构编码
        /// </summary>
        public bool AddJGBM { get; set; }
        #region 日志使用
        /// <summary>
        /// 请求者电脑唯一标识CPUID
        /// </summary>
        public string CPUID { get; set; }
        public string CurryTableName { get; set; }
        public string CurryKeyFiled { get; set; }
        public string MeunName { get; set; }
        public string ButtonName { get; set; }
        public string CurryXM { get; set; }
        public string CurryKEY { get; set; }
        public string DELREMARK { get; set; }
        public string MergeMainDHA { get; set; }
        public string CurryDHA { get; set; }
        public string ExecRemark { get; set; }

        #endregion

        #region 涉及到档案信息变更
        /// <summary>
        /// 本人电话是否变更
        /// </summary>
        public bool IsBrdh { get; set; }
        /// <summary>
        /// 本人电话
        /// </summary>
        public string BRDH { get; set; }
        /// <summary>
        /// 联系电话是否变更
        /// </summary>
        public bool IsLxdh { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// 联系人电话变更
        /// </summary>
        public bool IsLxrdh { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LXRDH { get; set; }

        /// <summary>
        /// 户籍流动情况变更
        /// </summary>
        public bool IsHjldqk { get; set; }
        /// <summary>
        /// 户籍流动情况
        /// </summary>
        public string HJLDQK { get; set; }

        #endregion

        #region 附件使用
        /// <summary>
        /// 主表业务guid
        /// </summary>
        public List<string> EZbguids { get; set; }
        /// <summary>
        /// GUID
        /// </summary>
        public List<string> Guids { get; set; }
        /// <summary>
        /// 附件表名称
        /// </summary>
        public string EtableName { get; set; }
        /// <summary>
        /// 附件主表名称
        /// </summary>
        public string EtableMainName
        {
            get
            {
                if (string.IsNullOrEmpty(EtableName))
                {
                    return "";
                }
                string str = EtableName.Substring(0, EtableName.LastIndexOf('_'));
                if (str == "T_SignIngAgreement")
                {
                    str = "T_AgreementDetailed";
                }
                if (str == "T_JKTJB")
                {
                    str = "T_JKTJBMAIN";
                }
                return str;
            }

        }
        /// <summary>
        /// 主表业务guid
        /// </summary>
        public string EZbguid { get; set; }
        /// <summary>
        /// 文件HashCode
        /// </summary>
        public List<string> EFileHashCode { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public List<string> EFileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public List<string> EFilelx { get; set; }
        /// <summary>
        /// 文件类容
        /// </summary>
        public List<object> EFiles { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public int Ejglx { get; set; }

        /// <summary>
        /// 附件摘要
        /// </summary>
        public List<string> EFJZYS { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public List<int> EFileWJLXs { get; set; }

        /// <summary>
        /// 是否修改主表标识
        /// </summary>
        public bool IsEditBs { get; set; }

        /// <summary>
        /// 是否根据主表guid删除附件
        /// </summary>
        public bool IsDropFile { get; set; }
        #endregion

        #region 参数化SQL使用
        /// <summary>
        /// 参数SQL值集合
        /// </summary>
        public List<List<object>> PARAMS { get; set; }
        /// <summary>
        /// 参数化SQL字段集合
        /// </summary>
        public List<List<string>> PARAMFILEDS { get; set; }
        #endregion

        /// <summary>
        /// 是否APP请求 仅用于APP请求
        /// </summary>
        public bool ISAPP { get; set; }

        /// <summary>
        /// 村卫生室机构编码
        /// </summary>
        public string ALJGBM { get; set; }

        /// <summary>
        /// 是否ISWEB请求 仅用于ISWEB请求
        /// </summary>
        public bool ISWEB { get; set; }

        /// <summary>
        /// 获取动态入参
        /// </summary>
        public List<string> paramStrs { get; set; }


        /// <summary>
        /// 机构ID(必传) 慢病独有参数  不会传输到公卫
        /// </summary>
        public int? ORGId { get; set; }
    }


    public class Base_Input<T> : BeRequest
    {
        /// <summary>
        /// 具体业务请求实体
        /// </summary>
        public T dataInput { get; set; }
    }
}
