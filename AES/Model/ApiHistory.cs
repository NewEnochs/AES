using SQLitePCL;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES.Model
{
    /// <summary>
    /// 定义实体（与 API_History 表对应）
    /// </summary>
    [SugarTable("API_History")]
    public class ApiHistory
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求模式
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestBody { get; set; }


        /// <summary>
        /// 相应参数
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// 状态 0.失败 1.成功
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        [SugarColumn(IsNullable = true)]
        public DateTime? CreatedTime { get; set; }


        // 在按钮点击中使用
        private async void btnTest_Click(object sender, EventArgs e)
        {
            using var context = new SqliteContext();

            try
            {
                // 插入一条记录
                var history = new ApiHistory
                {
                    RequestUrl = "https://api.example.com/data",
                    RequestMethod = "POST",
                    RequestBody = "{ \"key\": \"value\" }",
                    ResponseBody = "{ \"status\": \"ok\" }",
                    StatusCode = 200,
                    CreatedTime = DateTime.Now
                };

                var id = await context.Db.Insertable(history).ExecuteReturnIdentityAsync();
                MessageBox.Show($"插入成功，ID = {id}");

                // 查询所有
                var list = await context.Db.Queryable<ApiHistory>().ToListAsync();
                MessageBox.Show($"共 {list.Count} 条记录");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败: {ex.Message}");
            }
        }
    }
}