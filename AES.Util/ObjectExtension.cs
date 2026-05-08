using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AES
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 将对象序列化为 JSON 字符串
        /// </summary>
        public static string ToJson(this object obj, JsonSerializerOptions? options = null)
        {
            options ??= new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 可选：转为小驼峰
                WriteIndented = false,                             // 是否格式化
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // 忽略 null 值（可选）
            };

            return JsonSerializer.Serialize(obj, options);
        }
    }
}
