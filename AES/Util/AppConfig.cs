using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Util
{
    public class AppConfig
    {
        public List<string> Addresses { get; set; }
        public int DefaultIndex { get; set; }

        public string DefaultConnection { get; set; }

        // 加载配置文件
        public static AppConfig? LoadConfig()
        {
            string configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            if (!File.Exists(configPath))
            {
                // 如果配置文件不存在，创建默认配置
                var defaultConfig = new AppConfig
                {
                    Addresses = new List<string>
                    {
                        "http://localhost:8055",
                        "http://182.43.238.19:30017",
                        "http://40.2.39.48:36000"
                    },
                    DefaultIndex = 0
                };

                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            string json = File.ReadAllText(configPath);
            return json != null ? JsonConvert.DeserializeObject<AppConfig>(json) : new AppConfig();
        }

        // 保存配置文件
        public static void SaveConfig(AppConfig config)
        {
            string configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }

        public static AppConfig? LoadWebConfig()
        {
            string configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            if (!File.Exists(configPath))
            {
                // 如果配置文件不存在，创建默认配置
                var defaultConfig = new AppConfig
                {
                    Addresses = new List<string>
                    {
                        "http://localhost:83/api",
                        "http://182.43.238.19:30018/api",
                        "http://40.2.39.48:36002/api"
                    },
                    DefaultIndex = 0
                };

                SaveConfig(defaultConfig);
                return defaultConfig;
            }

            string json = File.ReadAllText(configPath);

            return json != null ? JsonConvert.DeserializeObject<AppConfig>(json) : new AppConfig();
        }
    }
}
