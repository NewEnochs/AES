using AES.Util;
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
        /// <summary>
        /// 原有的初始化方法：仅在数据库不存在时创建
        /// </summary>
        public static void CreateDatabaseAndTable()
        {
            string dbPath = GetDbPath();
            if (File.Exists(dbPath))
            {
                MessageBox.Show("数据库已存在,不能重复初始化");
                return;
            }

            ExecuteCreateTable(dbPath);
            MessageBox.Show($"数据库已创建：{dbPath}\n表 API_History 已创建或已存在。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 新增方法：清空并重新创建数据库
        /// </summary>
        public static void RecreateDatabase()
        {
            string dbPath = GetDbPath();

            try
            {
                // 1. 如果数据库文件存在，先将其删除
                if (File.Exists(dbPath))
                {
                    // 注意：删除前请确保没有任何程序或线程正在占用该数据库连接
                    File.Delete(dbPath);
                }

                // 2. 删除后，重新执行建库和建表逻辑
                ExecuteCreateTable(dbPath);

                MessageBox.Show($"数据库已成功重置！\n路径：{dbPath}", "重置成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"无法删除数据库文件，可能数据库正在被占用：{ioEx.Message}", "重置失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"重置数据库失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取数据库的完整物理路径
        /// </summary>
        private static string GetDbPath()
        {
            return DataPathHelper.GetDbPath();
        }

        /// <summary>
        /// 核心逻辑：创建数据库文件并执行建表SQL
        /// </summary>
        private static void ExecuteCreateTable(string dbPath)
        {
            string connectionString = $"Data Source={dbPath};";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableSql = @"
                CREATE TABLE IF NOT EXISTS API_History (
                    Id INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                    RequestRootPath VARCHAR(100),
                    RequestUrl TEXT,
                    RequestMethod TEXT,
                    RequestBody TEXT,
                    ResponseBody TEXT,
                    Token TEXT,
                    StatusCode INT,
                    CreatedTime DATETIME DEFAULT (datetime('now', 'localtime'))
                );";

                using (var command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}