using System;
using System.Data;
using System.Text;

namespace MyHelper4Web
{
    /// <summary>
    /// DataReader帮助类
    /// </summary>/// <returns>返回值</returns>
    public class MyDataReaderHelper
    {
        #region Public Fields

        private readonly DateTime _defaultDate;

        private readonly IDataReader _reader;

        #endregion

        #region Construcor

        /// <summary>
        /// 构造函数，传入IDataReader对象
        /// </summary>/// <returns>返回值</returns>
        /// <param name="reader">IDataReader 数据</param>
        public MyDataReaderHelper(IDataReader reader)
        {
            _defaultDate = Convert.ToDateTime("01/01/1970 00:00:00");
            _reader = reader;
        }

        #endregion

        #region Method

        /// <summary>
        /// 继续读取下一个操作
        /// </summary>
        /// <returns>返回值</returns>
        public bool Read()
        {
            return _reader.Read();
        }

        /// <summary>
        /// 转换为Int类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public int GetInt32(string column)
        {
            return GetInt32(column, 0);
        }

        /// <summary>
        /// 转换为Int类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public int GetInt32(string column, int defaultIfNull)
        {
            int data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                           ? defaultIfNull
                           : int.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Int类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public int? GetInt32Nullable(string column)
        {
            int? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                            ? (int?) null
                            : int.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Int16类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public short GetInt16(string column)
        {
            return GetInt16(column, 0);
        }

        /// <summary>
        /// 转换为Int16类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public short GetInt16(string column, short defaultIfNull)
        {
            short data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                             ? defaultIfNull
                             : short.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Int16类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public short? GetInt16Nullable(string column)
        {
            short? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                              ? (short?) null
                              : short.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Float类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public float GetFloat(string column)
        {
            return GetFloat(column, 0);
        }

        /// <summary>
        /// 转换为Float类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public float GetFloat(string column, float defaultIfNull)
        {
            float data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                             ? defaultIfNull
                             : float.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Float类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public float? GetFloatNullable(string column)
        {
            float? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                              ? (float?) null
                              : float.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Double类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public double GetDouble(string column)
        {
            return GetDouble(column, 0);
        }

        /// <summary>
        /// 转换为Double类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public double GetDouble(string column, double defaultIfNull)
        {
            double data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                              ? defaultIfNull
                              : double.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Double类型数据(可空类型）
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public double? GetDoubleNullable(string column)
        {
            double? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                               ? (double?) null
                               : double.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Decimal类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public decimal GetDecimal(string column)
        {
            return GetDecimal(column, 0);
        }

        /// <summary>
        /// 转换为Decimal类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public decimal GetDecimal(string column, decimal defaultIfNull)
        {
            decimal data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                               ? defaultIfNull
                               : decimal.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Decimal类型数据(可空类型）
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public decimal? GetDecimalNullable(string column)
        {
            decimal? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? (decimal?) null
                                : decimal.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Single类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public Single GetSingle(string column)
        {
            return GetSingle(column, 0);
        }

        /// <summary>
        /// 转换为Single类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public Single GetSingle(string column, Single defaultIfNull)
        {
            Single data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                              ? defaultIfNull
                              : Single.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为Single类型数据(可空类型）
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public Single? GetSingleNullable(string column)
        {
            Single? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                               ? (Single?) null
                               : Single.Parse(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为布尔类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public bool GetBoolean(string column)
        {
            return GetBoolean(column, false);
        }

        /// <summary>
        /// 转换为布尔类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public bool GetBoolean(string column, bool defaultIfNull)
        {
            string str = _reader[column].ToString();
            try
            {
                int i = Convert.ToInt32(str);
                return i > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 转换为布尔类型数据(可空类型）
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public bool? GetBooleanNullable(string column)
        {
            string str = _reader[column].ToString();
            try
            {
                int i = Convert.ToInt32(str);
                return i > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 转换为字符串类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public String GetString(string column)
        {
            return GetString(column, "");
        }

        /// <summary>
        /// 转换为字符串类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public string GetString(string column, string defaultIfNull)
        {
            string data = (_reader.IsDBNull(_reader.GetOrdinal(column))) ? defaultIfNull : _reader[column].ToString();
            return data;
        }

        /// <summary>
        /// 转换为Byte字节数据类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public byte[] GetBytes(string column)
        {
            return GetBytes(column, null);
        }

        /// <summary>
        /// 转换为Byte字节数据类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public byte[] GetBytes(string column, string defaultIfNull)
        {
            string data = (_reader.IsDBNull(_reader.GetOrdinal(column))) ? defaultIfNull : _reader[column].ToString();
            return Encoding.UTF8.GetBytes(data);
        }

        /// <summary>
        /// 转换为Guid类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public Guid GetGuid(string column)
        {
            return GetGuid(column, null);
        }

        /// <summary>
        /// 转换为Guid类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public Guid GetGuid(string column, string defaultIfNull)
        {
            string data = (_reader.IsDBNull(_reader.GetOrdinal(column))) ? defaultIfNull : _reader[column].ToString();
            Guid guid = Guid.Empty;
            if (data != null)
            {
                guid = new Guid(data);
            }
            return guid;
        }

        /// <summary>
        /// 转换为Guid类型数据(可空类型）
        /// </summary>/// <returns>返回值</returns> 
        /// <param name="column">列名</param>
        public Guid? GetGuidNullable(string column)
        {
            string data = (_reader.IsDBNull(_reader.GetOrdinal(column))) ? null : _reader[column].ToString();
            Guid? guid = null;
            if (data != null)
            {
                guid = new Guid(data);
            }
            return guid;
        }

        /// <summary>
        /// 转换为DateTime类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public DateTime GetDateTime(string column)
        {
            return GetDateTime(column, _defaultDate);
        }

        /// <summary>
        /// 转换为DateTime类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        /// <param name="defaultIfNull">如果为空的默认值</param>
        public DateTime GetDateTime(string column, DateTime defaultIfNull)
        {
            DateTime data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                ? defaultIfNull
                                : Convert.ToDateTime(_reader[column].ToString());
            return data;
        }

        /// <summary>
        /// 转换为可空DateTime类型数据
        /// </summary>
        /// <returns>返回值</returns>
        /// <param name="column">列名</param>
        public DateTime? GetDateTimeNullable(string column)
        {
            DateTime? data = (_reader.IsDBNull(_reader.GetOrdinal(column)))
                                 ? (DateTime?) null
                                 : Convert.ToDateTime(_reader[column].ToString());
            return data;
        }

        #endregion

    }
}
