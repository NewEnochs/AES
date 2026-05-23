using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Helper.ItemValueHelper
{
    public class ItemValueConfig
    {
        private readonly IConfiguration _config;

        public ItemValueConfig()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("itemValue.json", optional: false, reloadOnChange: true)
                .Build();
        }

        // 通用方法：根据配置节名称获取数据
        public List<T> GetData<T>(string sectionName)
        {
            return _config.GetSection(sectionName).Get<List<T>>() ?? new List<T>();
        }

        public string? GetData(string sectionName)
        {
            var model = _config.GetSection(sectionName);
            return model == null ? "" : model?.Value;
        }

        // 根据ID获取单个数据
        public T? GetDataById<T>(string sectionName, int id) where T : IHasId
        {
            var dataList = GetData<T>(sectionName);
            var model = dataList.FirstOrDefault(x => x.Id == id);
            return model == null ? default(T) : model;
        }

        // 根据条件获取数据
        public List<T> GetDataByCondition<T>(string sectionName, Func<T, bool> predicate)
        {
            var dataList = GetData<T>(sectionName);
            return dataList.Where(predicate).ToList();
        }

    }
}
