using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MyHelper4Web
{
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    /// <typeparam name="T">泛类</typeparam>
    public class MyDataTableHelper<T> where T : new()
    {
        #region Public Fields

        private static string _exceptionMessage; //定义异常信息

        #endregion

        #region Preperty

        /// <summary>
        /// 设置或取得异常信息
        /// </summary>
        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }

        #endregion

        #region Method

        /// <summary>
        /// 连接两个具有相同数据结构的DataTable,返回DataTable
        /// </summary>
        /// <param name="table1">表1</param>
        /// <param name="table2">表2</param>
        /// <returns>返回表</returns>
        public static DataTable GetInnerDataTable(DataTable table1, DataTable table2)
        {
            table1.Merge(table2);
            return table1;
        }

        /// <summary>
        /// 将数据集写入xml文件
        /// </summary>
        /// <param name="dataset">数据集</param>
        /// <param name="filename">XML文件名</param>
        /// <returns>是否成功</returns>
        public static bool WriteDataSetToXml(DataSet dataset, string filename)
        {
            try
            {
                dataset.WriteXml(filename);
                return true;
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 查询DataTable中的数据
        /// </summary>
        /// <param name="table">要查询的datatable</param>
        /// <param name="comText">查询条件</param>
        /// <returns>数据列集合</returns>
        public static DataRow[] GetSelectDataTable(DataTable table, string comText)
        {
            try
            {
                DataRow[] rows = table.Select(comText);
                return rows;
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 实体数组转DataTable
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="entitys">泛类对象</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            var dt = new DataTable();
            foreach (PropertyInfo t in entityProperties)
            {
                dt.Columns.Add(t.Name, t.PropertyType);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                var entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        /// DataTable转实体数组
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>泛类列表</returns>
        public static IList<T> DtToList(DataTable dt)
        {
            // 定义集合
            var ts = new List<T>();
            // 获得此模型的类型

            foreach (DataRow dr in dt.Rows)
            {
                var t = new T();
                // 获得此模型的公共属性
                var propertys = t.GetType().GetProperties();
                foreach (var pi in propertys)
                {
                    var tempName = pi.Name;
                    // 检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;
                        var value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 枚举转DataTable
        /// </summary>
        /// <param name="enumType">类型</param>
        /// <param name="key">索引</param>
        /// <param name="val">值</param>
        /// <returns>DataTable</returns>
        public static DataTable EnumToDataTable(Type enumType, string key, string val)
        {
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            var table = new DataTable();
            table.Columns.Add(key, Type.GetType("System.String"));
            table.Columns.Add(val, Type.GetType("System.Int32"));
            table.Columns[key].Unique = true;
            for (int i = 0; i < values.Length; i++)
            {
                var dr = table.NewRow();
                dr[key] = names[i];
                dr[val] = (int)values.GetValue(i);
                table.Rows.Add(dr);
            }
            return table;
        }

        /// <summary>
        /// 根据nameList里面的字段创建一个表格,返回该表格的DataTable
        /// </summary>
        /// <param name="nameList">包含字段信息的列表</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateTable(List<string> nameList)
        {
            if (nameList.Count <= 0)
                return null;

            var myDataTable = new DataTable();
            foreach (string columnName in nameList)
            {
                myDataTable.Columns.Add(columnName, typeof(string));
            }
            return myDataTable;
        }

        /// <summary>
        /// 通过字符列表创建表字段，字段格式可以是：
        /// 1) a,b,c,d,e
        /// 2) a|int,b|string,c|bool,d|decimal
        /// </summary>
        /// <param name="nameString">表名</param>
        /// <returns>数据表</returns>
        public static DataTable CreateTable(string nameString)
        {
            string[] nameArray = nameString.Split(new[] { ',', ';' });
            new List<string>();
            var dt = new DataTable();
            foreach (string item in nameArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] subItems = item.Split('|');
                    if (subItems.Length == 2)
                    {
                        dt.Columns.Add(subItems[0], ConvertType(subItems[1]));
                    }
                    else
                    {
                        dt.Columns.Add(subItems[0]);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="sorts">排序字段</param>
        /// <returns>排序后的表</returns>
        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = sorts.Aggregate("", (current, t) => current + (t + ","));
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }

        private static Type ConvertType(string typeName)
        {
            typeName = typeName.ToLower().Replace("system.", "");
            Type newType = typeof(string);
            switch (typeName)
            {
                case "boolean":
                case "bool":
                    newType = typeof(bool);
                    break;
                case "int16":
                case "short":
                    newType = typeof(short);
                    break;
                case "int32":
                case "int":
                    newType = typeof(int);
                    break;
                case "long":
                case "int64":
                    newType = typeof(long);
                    break;
                case "uint16":
                case "ushort":
                    newType = typeof(ushort);
                    break;
                case "uint32":
                case "uint":
                    newType = typeof(uint);
                    break;
                case "uint64":
                case "ulong":
                    newType = typeof(ulong);
                    break;
                case "single":
                case "float":
                    newType = typeof(float);
                    break;

                case "string":
                    newType = typeof(string);
                    break;
                case "guid":
                    newType = typeof(Guid);
                    break;
                case "decimal":
                    newType = typeof(decimal);
                    break;
                case "double":
                    newType = typeof(double);
                    break;
                case "datetime":
                    newType = typeof(DateTime);
                    break;
                case "byte":
                    newType = typeof(byte);
                    break;
                case "char":
                    newType = typeof(char);
                    break;
            }
            return newType;
        }


        #endregion

    }
}

