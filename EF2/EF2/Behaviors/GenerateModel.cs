using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EF2.Behaviors
{
    public static class GenerateModel
    {
        public static T Put<T>(T entity, DataRow row) where T : new() {
            if (null == entity) entity = new T();
            Type type = typeof(T);
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                var idx = row.Table.Columns.IndexOf(item.Name);
                if (idx < 0) continue;
                if (row[item.Name] != null && row[item.Name] != DBNull.Value) {
                    if (item.PropertyType == typeof(System.DateTime))
                    {
                        item.SetValue(entity, Convert.ToDateTime(row[item.Name]), null);
                    }
                    else {
                        item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                    }
                }
            }

            return entity;
               
        }

        public static List<T> PutAll<T>(T entity, DataSet ds) where T : new() {
            List<T> lst = new List<T>();
            if (null != ds && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    lst.Add(Put(new T(), dr));
                }
            }
            return lst;
        }


        public static T Query<T>(T entity, HttpRequestBase request) where T : new()
        {
            //初始化 如果为null
            if (entity == null) entity = new T();
            //得到类型
            Type type = typeof(T);
            //取得属性集合
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                if (string.IsNullOrWhiteSpace(request.QueryString[item.Name])) continue;
                var value = request.QueryString[item.Name];
                if (value == "null" || value == "undenfined") value = "";
                item.SetValue(entity, Convert.ChangeType(value, item.PropertyType), null);
            }
            return entity;
        }
    }
}
