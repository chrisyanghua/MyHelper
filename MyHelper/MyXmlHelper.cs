using System;
using System.Xml;

namespace MyHelper4Web
{
    /// <summary>
    /// XML 文件操作类
    /// </summary>
    public class MyXmlHelper
    {
        #region Method

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (xn != null)
                    if (xn.Attributes != null)
                        value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return value;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            XmlNode xn = doc.SelectSingleNode(node);
            if (element.Equals(""))
            {
                if (!attribute.Equals(""))
                {
                    var xe = (XmlElement)xn;
                    if (xe != null) xe.SetAttribute(attribute, value);
                }
            }
            else
            {
                XmlElement xe = doc.CreateElement(element);
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                if (xn != null) xn.AppendChild(xe);
            }
            doc.Save(path);
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                var xe = (XmlElement)xn;
                if (attribute.Equals(""))
                {
                    if (xe != null) xe.InnerText = value;
                }
                else if (xe != null) xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                var xe = (XmlElement)xn;
                if (attribute.Equals(""))
                {
                    if (xn != null) if (xn.ParentNode != null) xn.ParentNode.RemoveChild(xn);
                }
                else if (xe != null) xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch
            {
                return;
            }
        }
        
        #endregion
        #region add by frankie 201301291641
         /// <summary> 
    /// 读取XML返回经排序或筛选后的DataView
    /// </summary>
    /// <param name="strWhere">筛选条件，如:"name='kgdiwss'"</param>
    /// <param name="strSort"> 排序条件，如:"Id desc"</param>
    public DataView GetDataViewByXml(string strWhere, string strSort)
    {
        try
        {
            string XMLFile = this.XMLPath;
            string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
            DataSet ds = new DataSet();
            ds.ReadXml(filename);
            DataView dv = new DataView(ds.Tables[0]); //创建DataView来完成排序或筛选操作    
            if (strSort != null)
            {
                dv.Sort = strSort; //对DataView中的记录进行排序
            }
            if (strWhere != null)
            {
                dv.RowFilter = strWhere; //对DataView中的记录进行筛选，找到我们想要的记录
            }
            return dv;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// 读取XML返回DataSet
    /// </summary>
    /// <param name="strXmlPath">XML文件相对路径</param>
    public DataSet GetDataSetByXml(string strXmlPath)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(GetXmlFullPath(strXmlPath));
            if (ds.Tables.Count > 0)
            {
                return ds;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
        #endregion

    }
}
