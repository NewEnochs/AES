using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AES.Util
{
    public class AppConfig
    {
        // 1. 补全缺失的属性
        public List<string> Addresses { get; set; } = new List<string>();
        public List<string> WebAddress { get; set; } = new List<string>();
        public List<string> ChisAddress { get; set; } = new List<string>();
        public List<string> ChisWebAddress { get; set; } = new List<string>();
        public List<string> SMSAddress { get; set; } = new List<string>();
        public List<string> SMSWebAddress { get; set; } = new List<string>();
        public int DefaultIndex { get; set; }
        public string DefaultConnection { get; set; } = string.Empty;

        private static readonly string ConfigFileName = "appsettings.json";

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        private static string GetConfigPath()
        {
            // WinForms 中建议使用 AppContext.BaseDirectory 而不是 Directory.GetCurrentDirectory()
            // 因为后者可能受工作目录影响，而前者始终指向 exe 所在目录
            return Path.Combine(AppContext.BaseDirectory, ConfigFileName);
        }

        /// <summary>
        /// 加载配置文件 (单例模式或静态调用)
        /// </summary>
        public static AppConfig Load()
        {
            string configPath = GetConfigPath();

            if (!File.Exists(configPath))
            {
                var defaultConfig = CreateDefaultConfig();
                Save(defaultConfig);
                return defaultConfig;
            }

            try
            {
                string json = File.ReadAllText(configPath);
                var config = JsonConvert.DeserializeObject<AppConfig>(json);

                if (config == null)
                {
                    throw new Exception("未找到有效配置");
                }

                // 防御性编程：确保列表不为 null
                config.Addresses = config?.Addresses == null ? new List<string>() : config?.Addresses;
                config.WebAddress = config?.WebAddress == null ? new List<string>() : config?.WebAddress;

                return config ?? CreateDefaultConfig();
            }
            catch (Exception ex)
            {
                // 记录日志或抛出，这里简单处理为返回默认值并备份坏文件
                File.Copy(configPath, configPath + ".bak", true);
                var defaultConfig = CreateDefaultConfig();
                Save(defaultConfig);
                return defaultConfig;
            }
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void Save(AppConfig config)
        {
            string configPath = GetConfigPath();
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 创建默认配置
        /// </summary>
        private static AppConfig CreateDefaultConfig()
        {
            return new AppConfig
            {
                Addresses = new List<string>
                {
                    "http://localhost:8055",
                    "http://182.43.238.19:30017",
                    "http://40.2.39.48:36000"
                },
                WebAddress = new List<string>
                {
                    "http://localhost:83/api",
                    "http://182.43.238.19:30018/api",
                    "http://40.2.39.48:36002/api"
                },
                ChisAddress = new List<string>
                {
                    "http://localhost:5000/api",
                    "http://182.43.238.19:30023/api"
                },
                ChisWebAddress= new List<string> {
                    "http://localhost:81/api/api",
                    "http://182.43.238.19:30019/api/api"
                },
                DefaultIndex = 0,
                DefaultConnection = "Server=172.2.3.232;PORT=5236;User Id=HC_REGIONCDS;PWD=2x!31xGW#$pgOwBg"
            };
        }

        #region 便捷获取方法 (完善方案的核心)

        /// <summary>
        /// 获取当前默认的服务地址
        /// </summary>
        public string GetCurrentAddress()
        {
            if (Addresses == null || Addresses.Count == 0) return string.Empty;

            // 处理索引越界
            int index = (DefaultIndex >= 0 && DefaultIndex < Addresses.Count) ? DefaultIndex : 0;
            return Addresses[index];
        }

        /// <summary>
        /// 获取当前默认的 Web API 地址
        /// </summary>
        public string GetCurrentWebAddress()
        {
            if (WebAddress == null || WebAddress.Count == 0) return string.Empty;

            int index = (DefaultIndex >= 0 && DefaultIndex < WebAddress.Count) ? DefaultIndex : 0;
            return WebAddress[index];
        }

        /// <summary>
        /// 根据索引获取特定地址
        /// </summary>
        public string GetAddressByIndex(int index)
        {
            if (Addresses == null || index < 0 || index >= Addresses.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return Addresses[index];
        }

        /// <summary>
        /// 根据索引获取特定 Web 地址
        /// </summary>
        public string GetWebAddressByIndex(int index)
        {
            if (WebAddress == null || index < 0 || index >= WebAddress.Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            return WebAddress[index];
        }

        #endregion
    }
}