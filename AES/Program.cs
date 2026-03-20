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
            // 创建宿主环境，自动加载 appsettings.json
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // 确保读取当前目录下的 appsettings.json
                    config.SetBasePath(AppContext.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // 将配置绑定到 AppSettings 类
                    services.Configure<AppSettingsModel>(context.Configuration.GetSection("")); // 因为JSON根节点就是这些属性

                    // 注册你的 Form
                    services.AddSingleton<Form1>();
                })
                .Build();

            // 获取配置实例供全局使用（或者通过构造函数注入到 Form 中）
            var appSettings = host.Services.GetRequiredService<IOptions<AppSettingsModel>>().Value;

            //// 示例：在启动前验证配置
            //if (appSettings.Addresses.Count == 0)
            //{
            //    MessageBox.Show("配置文件加载失败或地址列表为空！");
            //    return;
            //}

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}