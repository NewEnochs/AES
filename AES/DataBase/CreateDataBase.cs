using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.DataBase
{
    /// <summary>
    /// 创建SQL Lite数据库 辅助类
    /// </summary>
    public class CreateDataBase
    {
        public static void CreateDatabaseAndTable()
        {
            // 1. 定义数据库文件路径（放在应用程序目录下）
            string dbFileName = "DBApi.db";
            string dbPath = Path.Combine(Application.StartupPath, dbFileName);

            // 2. 构建连接字符串
            string connectionString = $"Data Source={dbPath};";

            try
            {
                // 3. 创建数据库文件（如果不存在）并打开连接
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // 4. 创建 API_History 表（如果不存在）
                    string createTableSql = @"
                CREATE TABLE IF NOT EXISTS API_History (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    RequestUrl TEXT NOT NULL,
                    RequestMethod TEXT,
                    RequestBody TEXT,
                    ResponseBody TEXT,
                    Token TEXT,
                    CreatedTime DATETIME DEFAULT (datetime('now', 'localtime'))
                );";

                    using (var command = new SqliteCommand(createTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show($"数据库已创建：{dbPath}\n表 API_History 已创建或已存在。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"创建数据库失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
