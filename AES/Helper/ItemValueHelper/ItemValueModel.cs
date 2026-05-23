using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Helper
{
    internal class ItemValueModel
    {
    }

    /// <summary>
    /// 短信发送配置
    /// </summary>
    public class SmsData : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Mm { get; set; }
    }

    /// <summary>
    /// 数据库连接配置
    /// </summary>
    public class ConnStringData : DBConn, IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DBConn> connData { get; set; }
    }

    public class DBConn
    {
        public string ConnString { get; set; }
        public string DbType { get; set; }
        public string DbTypeName { get; set; }
    }

    public class ItemValuesData
    {
        public List<SmsData> SmsData { get; set; }
        public List<ConnStringData> ConnStringData { get; set; }
    }


    // 定义接口约束
    public interface IHasId
    {
        int Id { get; set; }
    }
}
