using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Model
{

    public class SqliteContext : IDisposable
    {
        public SqlSugarClient Db { get; private set; }

        public SqliteContext()
        {
            // 1. 数据库文件路径：程序启动目录下的 DBApi.db
            string dbPath = Path.Combine(Application.StartupPath, "DBApi.db");
            string connectionString = $"Data Source={dbPath};";

            var config = new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true, // 自动关闭连接，避免连接泄漏
                InitKeyType = InitKeyType.Attribute // 从实体特性读取主键/自增等
            };

            Db = new SqlSugarClient(config);

            // 全局 SQL 执行错误事件（更通用的异常捕获）
            Db.Aop.OnLogExecuted = (sql, pars) =>
            {
                // 可选：记录执行开始（一般不需要）
            };
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                // OnLogExecuted 已通过 ConfigureExternalServices.LogExecuted 处理
            };
            Db.Aop.OnError = (exp) =>
            {
                string fullSql = LogHelper.GetFullSql(exp.Sql, (SugarParameter[])exp.Parametres);

                StringBuilder callStackLog = new StringBuilder();
                callStackLog.AppendLine("\n【SQL 执行信息】");
                callStackLog.AppendLine($"┣ SQL 语句：{fullSql}");
                callStackLog.AppendLine($"┣ SQL 错误提示：{exp.Message}");
                callStackLog.AppendLine($"┣ 执行耗时：{Db.Ado.SqlExecutionTime.TotalMilliseconds} ms");
                // 获取完整调用堆栈
                var stackTrace = new System.Diagnostics.StackTrace(true);
                var frames = stackTrace.GetFrames();
                if (frames != null)
                {
                    for (int i = 0; i < frames.Length; i++)
                    {
                        var frame = frames[i];
                        var method = frame.GetMethod();
                        var declaringType = method?.DeclaringType;
                        var fileName1 = frame.GetFileName();
                        var fileLine1 = frame.GetFileLineNumber();

                        callStackLog.AppendLine($"┣ 调用层级 {i + 1}：{declaringType?.FullName}.{method?.Name}");

                        callStackLog.AppendLine($"  ┗ 文件路径：{fileName1}");
                        callStackLog.AppendLine($"  ┗ 代码行号：{fileLine1}");

                    }
                }
            };

            // 可选：设置命令超时（秒）
            Db.Ado.CommandTimeOut = 30;
        }

        // 确保资源释放（虽然 IsAutoCloseConnection=true，但显式 Dispose 更安全）
        public void Dispose()
        {
            Db?.Dispose();
        }
    }

}
