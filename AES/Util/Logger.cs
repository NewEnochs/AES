using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AES
{
    public static class Logger
    {
        private static readonly object _syncRoot = new object();

        // 缓存 Logger 实例，Key = logName + 日期
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, Serilog.ILogger> _loggers
            = new System.Collections.Concurrent.ConcurrentDictionary<string, Serilog.ILogger>();

        private static Serilog.ILogger GetLogger(string logName)
        {
            string dateKey = DateTime.Now.ToString("yyyy-MM-dd");
            string key = $"{logName}_{dateKey}";

            if (_loggers.TryGetValue(key, out var logger))
                return logger;

            lock (_syncRoot)
            {
                if (_loggers.TryGetValue(key, out logger))
                    return logger;

                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", dateKey);
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                string filePath = System.IO.Path.Combine(path, $"{logName}.log");

                // 使用 Async Sink，实现异步批量写入
                logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Async(a => a.File(
                        path: filePath,
                        rollingInterval: RollingInterval.Infinite,
                        retainedFileCountLimit: null,
                        fileSizeLimitBytes: 50 * 1024 * 1024, // 50MB
                        rollOnFileSizeLimit: true,
                        shared: true,
                        outputTemplate: "{Message}{NewLine}"
                    ))
                    .CreateLogger();

                _loggers[key] = logger;
                return logger;
            }
        }

        private static void WriteInternal(string action, string strMessage, string logName)
        {
            var logger = GetLogger(logName);
            var time = DateTime.Now;

            var logText = new System.Text.StringBuilder();
            logText.AppendLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            logText.AppendLine($"┣ Time: {time:yyyy-MM-dd HH:mm:ss.fff}");
            if (!string.IsNullOrEmpty(action))
                logText.AppendLine($"┣ Action: {action}");
            logText.AppendLine($"┣ Message: {strMessage}");
            logText.AppendLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\r\n");

            // 异步写入，由 Async Sink 完成
            logger.Information(logText.ToString());
        }

        // 公共接口
        public static void Info(string action, string strMessage) => WriteInternal(action, strMessage, "info");
        public static void Error(string action, string strMessage) => WriteInternal(action, strMessage, "error");

        public static void Error(Exception ex)
        {
            if (ex == null) return;
            Error("", ex.Message);
            if (!string.IsNullOrEmpty(ex.StackTrace))
                Error("", ex.StackTrace);
        }

        public static void Write(string action, string strMessage, string logName = "other")
            => WriteInternal(action, strMessage, logName);

        public static void Write(string strMessage, string logName = "other")
            => WriteInternal("", strMessage, logName);

        public static void WriteEmpty(string strMessage, string logName = "other")
            => WriteInternal("", strMessage, logName);
    }
}

