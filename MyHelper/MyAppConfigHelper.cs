using System;
using System.IO;
using System.Xml;

namespace MyHelper4Web
{
    /// <summary>
    /// 配置文件操作类 
    /// </summary>
    public class MyAppConfigHelper
    {
        #region Public Fields

        private readonly string _filePath;

        #endregion

        #region Construcor

        /// <summary>
        /// 用户指定具体的配置文件路径
        /// </summary>
        /// <param name="configFilePath">配置文件路径（绝对路径）</param>
        public MyAppConfigHelper(string configFilePath)
        {
            string webconfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFilePath);

            if (File.Exists(webconfig))
            {
                _filePath = webconfig;
            }
            else
            {
                throw new ArgumentNullException(string.Format("{0}没有找到Web.Config文件或者应用程序配置文件, 请指定配置文件", "ARG0"));
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 设置程序的config文件
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="keyValue">键值</param>
        public void AppConfigSet(string keyName, string keyValue)
        {
            var document = new XmlDocument();
            document.Load(_filePath);

            XmlNodeList nodes = document.GetElementsByTagName("add");
            for (var i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                var xmlAttributeCollection = nodes[i].Attributes;
                if (xmlAttributeCollection != null)
                {
                    XmlAttribute attribute = xmlAttributeCollection["key"];
                    //根据元素的第一个属性来判断当前的元素是不是目标元素
                    if (attribute != null && (attribute.Value == keyName))
                    {
                        attribute = xmlAttributeCollection["value"];
                        //对目标元素中的第二个属性赋值
                        if (attribute != null)
                        {
                            attribute.Value = keyValue;
                            break;
                        }
                    }
                }
            }
            document.Save(_filePath);
        }

        /// <summary>
        /// 读取程序的config文件的键值。
        /// 如果键名不存在，返回空
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns>键值</returns>
        public string AppConfigGet(string keyName)
        {
            string strReturn = string.Empty;
            try
            {
                var document = new XmlDocument();
                document.Load(_filePath);

                XmlNodeList nodes = document.GetElementsByTagName("add");
                for (var i = 0; i < nodes.Count; i++)
                {
                    //获得将当前元素的key属性
                    var xmlAttributeCollection = nodes[i].Attributes;
                    if (xmlAttributeCollection != null)
                    {
                        XmlAttribute attribute = xmlAttributeCollection["key"];
                        //根据元素的第一个属性来判断当前的元素是不是目标元素
                        if (attribute != null && (attribute.Value == keyName))
                        {
                            attribute = xmlAttributeCollection["value"];
                            if (attribute != null)
                            {
                                strReturn = attribute.Value;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }

            return strReturn;
        }

        /// <summary>
        /// 获取指定键名中的子项的值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="subKeyName">以分号(;)为分隔符的子项名称</param>
        /// <returns>对应子项名称的值（即是=号后面的值）</returns>
        public string GetSubValue(string keyName, string subKeyName)
        {
            string connectionString = AppConfigGet(keyName).ToLower();
            string[] item = connectionString.Split(new[] { ';' });

            foreach (string t in item)
            {
                string itemValue = t.ToLower();
                if (itemValue.IndexOf(subKeyName.ToLower()) >= 0) //如果含有指定的关键字
                {
                    int startIndex = t.IndexOf("="); //等号开始的位置
                    return t.Substring(startIndex, 1); //获取等号后面的值即为Value
                }
            }
            return string.Empty;
        }

        #endregion

    }
}
