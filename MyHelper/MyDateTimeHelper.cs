using System;
using System.Globalization;

namespace MyHelper4Web
{
    /// <summary>
    /// 时间操作类
    /// </summary>
    public class MyDateTimeHelper
    {
        #region Method

        /// <summary>
        /// 哪天
        /// </summary>
        /// <param name="days">7天前:-7 7天后:+7</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetTheDay(int? days, DateTime dateTime)
        {
            int day = days ?? 0;
            return dateTime.AddDays(day).ToShortDateString();
        }

        /// <summary>
        /// 周日
        /// </summary>
        /// <param name="weeks">上周-1 下周+1 本周0</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetSunday(int? weeks, DateTime dateTime)
        {
            int week = weeks ?? 0;
            return dateTime.AddDays(Convert.ToDouble((0 - Convert.ToInt16(dateTime.DayOfWeek))) + 7 * week).ToShortDateString();
        }

        /// <summary>
        /// 周六
        /// </summary>
        /// <param name="weeks">上周-1 下周+1 本周0</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetSaturday(int? weeks, DateTime dateTime)
        {
            int week = weeks ?? 0;
            return dateTime.AddDays(Convert.ToDouble((6 - Convert.ToInt16(dateTime.DayOfWeek))) + 7 * week).ToShortDateString();
        }

        /// <summary>
        /// 月第一天
        /// </summary>
        /// <param name="months">上月-1 本月0 下月1</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetFirstDayOfMonth(int? months, DateTime dateTime)
        {
            int month = months ?? 0;
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(month).ToShortDateString();
        }

        /// <summary>
        /// 月最后一天
        /// </summary>
        /// <param name="months">上月0 本月1 下月2</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetLastDayOfMonth(int? months, DateTime dateTime)
        {
            int month = months ?? 0;
            return DateTime.Parse(dateTime.ToString("yyyy-MM-01")).AddMonths(month).AddDays(-1).ToShortDateString();
        }

        /// <summary>
        /// 年度第一天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetFirstDayOfYear(DateTime dateTime)
        {
            int year = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(year).ToShortDateString();
        }

        /// <summary>
        /// 年度最后一天
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetLastDayOfYear(DateTime dateTime)
        {
            int year = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(year).AddDays(-1).ToShortDateString();
        }

        /// <summary>
        /// 季度第一天
        /// </summary>
        /// <param name="quarters">上季度-1 下季度+1</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetFirstDayOfQuarter(int? quarters, DateTime dateTime)
        {
            int quarter = quarters ?? 0;
            return dateTime.AddMonths(quarter * 3 - ((dateTime.Month - 1) % 3)).ToString("yyyy-MM-01");
        }

        /// <summary>
        /// 季度最后一天
        /// </summary>
        /// <param name="quarters">上季度0 本季度1 下季度2</param>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetLastDayOfQuarter(int? quarters, DateTime dateTime)
        {
            int quarter = quarters ?? 0;
            return
                DateTime.Parse(dateTime.AddMonths(quarter * 3 - ((dateTime.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).
                    ToShortDateString();
        }

        /// <summary>
        /// 中文星期
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetDayOfWeekCN(DateTime dateTime)
        {
            var day = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return day[Convert.ToInt16(dateTime.DayOfWeek)];
        }

        /// <summary>
        /// 获取星期数字形式,周一开始
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>返回值</returns>
        public int GetDayOfWeekNum(DateTime dateTime)
        {
            int day = (Convert.ToInt16(dateTime.DayOfWeek) == 0) ? 7 : Convert.ToInt16(dateTime.DayOfWeek);
            return day;
        }

        /// <summary> 
        /// 取指定日期是一年中的第几周 
        /// </summary> 
        /// <param name="dtime">日期时间</param> 
        /// <returns>数字 一年中的第几周</returns> 
        public static int GetWeekofyear(DateTime dtime)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dtime, CalendarWeekRule.FirstDay,
                                                                     DayOfWeek.Sunday);
        }

        #endregion

    }
}

#region MyRegion

//dt.ToString();//2005-11-5 13:21:25
//dt.ToFileTime().ToString();//127756416859912816
//dt.ToFileTimeUtc().ToString();//127756704859912816
//dt.ToLocalTime().ToString();//2005-11-5 21:21:25
//dt.ToLongDateString().ToString();//2005年11月5日
//dt.ToLongTimeString().ToString();//13:21:25
//dt.ToOADate().ToString();//38661.5565508218
//dt.ToShortDateString().ToString();//2005-11-5
//dt.ToShortTimeString().ToString();//13:21
//dt.ToUniversalTime().ToString();//2005-11-5 5:21:25
//dt.Year.ToString();//2005
//dt.Date.ToString();//2005-11-5 0:00:00
//dt.DayOfWeek.ToString();//Saturday
//dt.DayOfYear.ToString();//309
//dt.Hour.ToString();//13
//dt.Millisecond.ToString();//441
//dt.Minute.ToString();//30
//dt.Month.ToString();//11
//dt.Second.ToString();//28
//dt.Ticks.ToString();//632667942284412864
//dt.TimeOfDay.ToString();//13:30:28.4412864
//dt.ToString();//2005-11-5 13:47:04
//dt.AddYears(1).ToString();//2006-11-5 13:47:04
//dt.AddDays(1.1).ToString();//2005-11-6 16:11:04
//dt.AddHours(1.1).ToString();//2005-11-5 14:53:04
//dt.AddMilliseconds(1.1).ToString();//2005-11-5 13:47:04
//dt.AddMonths(1).ToString();//2005-12-5 13:47:04
//dt.AddSeconds(1.1).ToString();//2005-11-5 13:47:05
//dt.AddMinutes(1.1).ToString();//2005-11-5 13:48:10
//dt.AddTicks(1000).ToString();//2005-11-5 13:47:04
//dt.CompareTo(dt).ToString();//0
//dt.Add(?).ToString();//问号为一个时间段
//dt.Equals("2005-11-6 16:11:04").ToString();//False
//dt.Equals(dt).ToString();//True
//dt.GetHashCode().ToString();//1474088234
//dt.GetType().ToString();//System.DateTime
//dt.GetTypeCode().ToString();//DateTime

//dt.GetDateTimeFormats('s')[0].ToString();//2005-11-05T14:06:25
//dt.GetDateTimeFormats('t')[0].ToString();//14:06
//dt.GetDateTimeFormats('y')[0].ToString();//2005年11月
//dt.GetDateTimeFormats('D')[0].ToString();//2005年11月5日
//dt.GetDateTimeFormats('D')[1].ToString();//2005 11 05
//dt.GetDateTimeFormats('D')[2].ToString();//星期六 2005 11 05
//dt.GetDateTimeFormats('D')[3].ToString();//星期六 2005年11月5日
//dt.GetDateTimeFormats('M')[0].ToString();//11月5日
//dt.GetDateTimeFormats('f')[0].ToString();//2005年11月5日 14:06
//dt.GetDateTimeFormats('g')[0].ToString();//2005-11-5 14:06
//dt.GetDateTimeFormats('r')[0].ToString();//Sat, 05 Nov 2005 14:06:25 GMT

//string.Format("{0:d}",dt);//2005-11-5
//string.Format("{0:D}",dt);//2005年11月5日
//string.Format("{0:f}",dt);//2005年11月5日 14:23
//string.Format("{0:F}",dt);//2005年11月5日 14:23:23
//string.Format("{0:g}",dt);//2005-11-5 14:23
//string.Format("{0:G}",dt);//2005-11-5 14:23:23
//string.Format("{0:M}",dt);//11月5日
//string.Format("{0:R}",dt);//Sat, 05 Nov 2005 14:23:23 GMT
//string.Format("{0:s}",dt);//2005-11-05T14:23:23
//string.Format("{0:t}",dt);//14:23
//string.Format("{0:T}",dt);//14:23:23
//string.Format("{0:u}",dt);//2005-11-05 14:23:23Z
//string.Format("{0:U}",dt);//2005年11月5日 6:23:23
//string.Format("{0:Y}",dt);//2005年11月
//string.Format("{0}",dt);//2005-11-5 14:23:23
//string.Format("{0:yyyyMMddHHmmssffff}",dt); 

#endregion
