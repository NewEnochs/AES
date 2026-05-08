using SQLitePCL;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DBEntity.Model
{
    /// <summary>
    /// 定义实体（与 API_History 表对应）
    /// </summary>
    [SugarTable("API_History")]
    public class ApiHistory
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string? RequestRootPath { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string? RequestUrl { get; set; }

        /// <summary>
        /// 请求模式
        /// </summary>
        public string? RequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? RequestBody { get; set; }


        /// <summary>
        /// 响应参数
        /// </summary>
        public string? ResponseBody { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }

    }
}