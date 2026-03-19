using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AES.Model
{
    /// <summary>
    /// 自定义实体基类
    /// </summary>
    public abstract class DEntityBase 
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int? Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public virtual int? CreatedUserId { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        public virtual string CreatedUserName { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public virtual int? UpdatedUserId { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        public virtual string UpdatedUserName { get; set; }
    }
}
