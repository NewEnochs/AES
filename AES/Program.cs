using AES.Helper;
using AES.Helper.ItemValueHelper;
using AES.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AES
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string zxlx = "";
            // 创建宿主环境
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // 确保读取当前目录下的 itemValue.json
                    config.SetBasePath(AppContext.BaseDirectory)
                          .AddJsonFile("itemValue.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // 绑定配置到实体类
                    services.Configure<ItemValuesData>(context.Configuration);

                    // 注册配置服务（方便获取配置实例）
                    services.AddSingleton<IItemValueService, ItemValueService>();

                     zxlx = new ItemValueConfig().GetData("ZXLX");
                    if (zxlx == "0")
                    {
                        services.AddSingleton<ExecuteSQLForm>();
                    }
                    else
                    {
                        // 注册你的 Form（通过服务提供者创建，这样才能注入依赖）
                        services.AddSingleton<Form1>();
                    }
                })
                .Build();

            // 运行应用程序
            ApplicationConfiguration.Initialize();

            if (zxlx == "0")
            {
                var form1 = host.Services.GetRequiredService<ExecuteSQLForm>();
                Application.Run(form1);
            }
            else
            {
                // 从服务容器中获取 Form1 实例
                var form1 = host.Services.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }
    }
}