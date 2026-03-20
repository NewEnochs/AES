using AES.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AES.Model
{
    public class DbContext
    {
        string conn = AppConfig.Load().DefaultConnection;

        //private ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(ChisDbContext));
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conn,
                DbType = DbType.Dm,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            });

            Db.Ado.CommandTimeOut = 180;
            Db.Aop.OnLogExecuted = (sql, pars) =>
            {
                Logger.Write(sql, "SqlSugarSQL");

                //代码CS文件名
                var fileName = Db.Ado.SqlStackTrace.FirstFileName;
                //代码行数
                var fileLine = Db.Ado.SqlStackTrace.FirstLine;
                //方法名
                var FirstMethodName = Db.Ado.SqlStackTrace.FirstMethodName;

                //执行时间超过1秒
                if (Db.Ado.SqlExecutionTime.TotalSeconds > 1)
                {
                    //Logger.Write("┣ 方法名称：" + fileName + "\r┣ 方法名：" + FirstMethodName + "\r┣ 代码行数：" + fileLine + "\r┣ SQL语句：" + sql + "\r┣ 耗时：" + db.Ado.SqlExecutionTime.TotalSeconds.ToString(), "sql");

                    //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息
                }
#if DEBUG
                Logger.Write("┣ 方法名称：" + fileName + "\r┣ 方法名：" + FirstMethodName + "\r┣ 代码行数：" + fileLine + "\r┣ SQL语句：" + sql + "\r┣ 耗时：" + Db.Ado.SqlExecutionTime.TotalSeconds.ToString(), "sql");
#endif
                // 获取完整调用堆栈
                var stackTrace = new System.Diagnostics.StackTrace(true);
                var frames = stackTrace.GetFrames();

                // 生成调用链日志
                StringBuilder callStackLog = new StringBuilder();
                callStackLog.AppendLine("\n【SQL 执行信息】");
                callStackLog.AppendLine($"┣ SQL 语句：{sql}");
                callStackLog.AppendLine($"┣ 执行耗时：{Db.Ado.SqlExecutionTime.TotalMilliseconds} ms");

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
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            callStackLog.AppendLine($"  ┗ 文件路径：{fileName1}");
                            callStackLog.AppendLine($"  ┗ 代码行号：{fileLine1}");
                        }
                    }
                }
                Logger.Write(callStackLog.ToString(), "sql");


            };
            Db.Aop.OnError = (exp) =>//执行SQL 错误事件
            {
                //Logger.Write("SqlSugar", UtilMethods.GetNativeSql(exp.Sql, exp.Parametres));
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
                //获取原生SQL推荐 5.1.4.63  性能OK
                //UtilMethods.GetNativeSql(exp.sql,exp.parameters)

                //获取无参数SQL对性能有影响，特别大的SQL参数多的，调试使用
                // UtilMethods.GetSqlString(DbType.SqlServer,exp.Sql, exp.Parametres)          
            };
        }
        //注意：不能写成静态的，不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        public IAdo Ado
        {
            get { return Db.Ado; }
            set { }
        }
    }


    public static class LogHelper
    {
        public static string GetFullSql(string sql, SugarParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return sql;

            foreach (var param in parameters)
            {
                string paramName = param.ParameterName.TrimStart('@'); // 移除 `@` 符号，确保匹配

                string valueStr;
                if (param.Value == null || param.Value == DBNull.Value)
                {
                    valueStr = "NULL";
                }
                else if (param.Value is string || param.Value is DateTime)
                {
                    valueStr = $"'{param.Value.ToString().Replace("'", "''")}'"; // 处理 SQL 转义
                }
                else
                {
                    valueStr = param.Value.ToString(); // 其他数据类型直接转换
                }

                // 使用正则确保完整替换参数
                sql = Regex.Replace(sql, $@"\B@{paramName}\b", valueStr);
            }
            return sql;
        }
    }


    public class ClaimConst
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public const string CLAINM_USERID = "UserId";

        /// <summary>
        /// 账号
        /// </summary>
        public const string CLAINM_ACCOUNT = "Account";

        /// <summary>
        /// 授权账户
        /// </summary>
        public const string ROOT_USERID = "RootUserId";

        /// <summary>
        /// 名称
        /// </summary>
        public const string CLAINM_NAME = "Name";

        /// <summary>
        /// 是否超级管理
        /// </summary>
        public const string CLAINM_SUPERADMIN = "SuperAdmin";

        /// <summary>
        /// 授权账户是否超级管理
        /// </summary>
        public const string CLAINM_ROOT_SUPERADMIN = "RootSuperAdmin";

    }
}
