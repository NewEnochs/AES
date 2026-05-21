using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AES.Helper
{
    public static class DataHelper
    {
        public static T ToObject<T>(this string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// table转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DataTable dt)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// josn转换成Hashtable
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static Hashtable JsonToHashtable(this string json)
        {
            return JsonConvert.DeserializeObject<Hashtable>(json);

        }
        /// <summary>
        /// json转xml
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static XmlNode JsonToXml(this string json)
        {
            return JsonConvert.DeserializeXmlNode(json);

        }
        /// <summary>
        /// 将object转换成为string
        /// </summary>
        /// <param name="ob">obj对象</param>
        /// <returns></returns>
        public static string ObjToStr(object ob)
        {
            if (ob == null)
            {
                return string.Empty;
            }
            else
                return ob.ToString();
        }
        /// <summary>
        /// JSON转datatable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable ToTable(this string json)
        {
            return JsonConvert.DeserializeObject<DataTable>(json);
        }

        /// <summary>
        /// JSON转Datasset
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(this string json)
        {
            return JsonConvert.DeserializeObject<DataSet>(json);
        }


        /// <summary>
        /// 读文件数据流
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static byte[] GetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源 
                    fs.Close();
                }
            }
        }
    }
}
