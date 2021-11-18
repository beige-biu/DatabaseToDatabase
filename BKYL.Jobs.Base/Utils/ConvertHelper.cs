using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace BKYL.Jobs.Base.Utils
{
    public class ConvertHelper<T> where T : new()
    {
        /// <summary>
        ///     利用反射和泛型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToList(DataTable dt)
        {
            // 定义集合
            var ts = new List<T>();
            // 获得此模型的类型
            var type = typeof (T);
            //定义一个临时变量
            var tempName = string.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                // 获得此模型的公共属性
                var propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (var pi in propertys)
                {
                    tempName = pi.Name; //将属性名称赋值给临时变量
                    //检查DataTable是否包含此列(列名==对象的属性名)
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue; //该属性不可写，直接跳出
                        //取值
                        var value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            try
                            {
                                pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType), null);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }

        public static DataTable ConvertToDT(List<T> list)
        {
            var table = CreateTable();
            var entityType = typeof (T);
            var properties = TypeDescriptor.GetProperties(entityType);
            foreach (var item in list)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        private static DataTable CreateTable()
        {
            var entityType = typeof (T);
            var table = new DataTable(entityType.Name);
            var properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
    }
}