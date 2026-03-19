using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Helper
{
    /// <summary>
    /// HTTP API解析
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 状态（0：失败，1：成功）
        /// </summary>
        public bool _state;
        public bool state { get { return success; } set { _state = value; } }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// 返回数据集，json格式
        /// </summary>
        public string? data
        {
            get; set;
        }

        /// <summary>
        /// 数据查询总行数
        /// </summary>
        public int icount { get; set; }
        /// <summary>
        /// 主键值，用于新增，修改操作业务后使用
        /// </summary>
        public string? key { get; set; }


        public int? code { get; set; }

        public bool success { get; set; }

    }

}