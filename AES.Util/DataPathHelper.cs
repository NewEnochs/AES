using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Util
{
    public static class DataPathHelper
    {
        private static readonly string DbFileName = "DBApi.db";
        /// <summary>
        /// 获取数据库的完整物理路径
        /// </summary>
        public static string GetDbPath()
        {
            var rootPath = AppDomain.CurrentDomain.BaseDirectory + "db";
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            return Path.Combine(rootPath + "\\", DbFileName);
        }
    }
}
