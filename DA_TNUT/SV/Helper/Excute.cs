using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV.Helper
{
    public static class Excute
    {
        public static string ThuTrongTuan(DateTime date)
        {
            var dayOfweek = date.DayOfWeek;
            switch (dayOfweek)
            {
                case DayOfWeek.Monday:
                    {
                        return "Thứ 2";
                    }
                case DayOfWeek.Tuesday:
                    {
                        return "Thứ 3";
                    }
                case DayOfWeek.Wednesday:
                    {
                        return "Thứ 4";
                    }
                case DayOfWeek.Thursday:
                    {
                        return "Thứ 5";
                    }
                case DayOfWeek.Friday:
                    {
                        return "Thứ 6";
                    }
                case DayOfWeek.Saturday:
                    {
                        return "Thứ 7";
                    }
                case DayOfWeek.Sunday:
                    {
                        return "Chủ nhật";
                    } 
                default:
                    {
                        return "";
                    }
            }
        }
        public static List<string> SplitString(string data)
        {
            List<string> listColl = new List<string>();
            listColl.AddRange(data.Split(',').Select(m => m.Trim()).Where(m => m != "").ToList());
            return listColl;
        }
        public static string ViewHistoryDate(DateTime historyTime)
        {
            var timeNow = DateTime.Now;
            var timeSpan = timeNow - historyTime;
            if (timeSpan.TotalDays / 365 >= 1)
            {
                return Convert.ToInt32(timeSpan.TotalDays / 365) + " năm trước";
            }
            if (timeSpan.TotalDays >= 30)
            {
                return (int)timeSpan.TotalDays / 30 + " tháng trước";
            }
            if (timeSpan.TotalDays >= 1)
            {
                return (int)timeSpan.TotalDays + " ngày trước";
            }
            if (timeSpan.TotalHours >= 1)
            {
                return (int)timeSpan.TotalHours + " giờ trước";
            }
            if (timeSpan.TotalMinutes > 0)
            {
                return (int)timeSpan.TotalMinutes + " phút trước";
            }
            return "Vừa đăng";
        }
        public static string ViewFutureDate(DateTime futureTime)
        {
            var timeNow = DateTime.Now;
            var timeSpan = futureTime - timeNow;
            if (timeSpan.TotalDays / 365 >= 1)
            {
                return Convert.ToInt32(timeSpan.TotalDays / 365) + " năm";
            }
            if (timeSpan.TotalDays >= 30)
            {
                return (int)timeSpan.TotalDays / 30 + " tháng";
            }
            if (timeSpan.TotalDays >= 1)
            {
                return (int)timeSpan.TotalDays + " ngày";
            }
            if (timeSpan.TotalHours >= 1)
            {
                return (int)timeSpan.TotalHours + " giờ";
            }
            if (timeSpan.TotalMinutes > 0)
            {
                return (int)timeSpan.TotalMinutes + " phút";
            }
            return "Hết hạn";
        }
    }
}