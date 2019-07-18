using System;

namespace XSchool.Helpers
{
    public class TimeHelper
    {
        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime target)
        {
            return DateTime.Parse(target.AddDays(1 - target.Day).ToString("yyyy-MM-dd"));
        }

        //// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime target)
        {
            return DateTime.Parse(target.AddDays(1 - target.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"));
        }

        //// <summary>
        /// 取得上个月第一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfPreviousMonth(DateTime target)
        {
            return DateTime.Parse(target.AddDays(1 - target.Day).AddMonths(-1).ToString("yyyy-MM-dd"));
        }

        //// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime LastDayOfPrdviousMonth(DateTime target)
        {
            return DateTime.Parse(target.AddDays(1 - target.Day).AddDays(-1).ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 获取该时间所在季度的第一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToFirstDayOfSeason(DateTime target)
        {
            int ThisMonth = DateTime.Now.Month;
            int FirstMonthOfSeason = ThisMonth - (ThisMonth % 3 == 0 ? 3 : (ThisMonth % 3)) + 1;
            target = target.AddMonths(FirstMonthOfSeason - ThisMonth);

            return DateTime.Parse(target.ToString("yyyy-MM-01"));
        }

        /// <summary>
        /// 获取该时间所在季度的最后一天
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToLastDayOfSeason(DateTime target)
        {
            int ThisMonth = DateTime.Now.Month;
            int FirstMonthOfSeason = ThisMonth - (ThisMonth % 3 == 0 ? 3 : (ThisMonth % 3)) + 3;
            target = target.AddMonths(FirstMonthOfSeason - ThisMonth);

            return DateTime.Parse(target.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
        }

        /// <summary>
        /// 获取该时间所在的季度
        /// </summary>
        /// <param name="target"></param>
        /// <returns>2018Q1</returns>
        public static int GetQurater(DateTime target)
        {
            int month = target.Month;
            if (month == 1 || month == 2 || month == 3)
            {
                return 1;
            }
            else if (month == 4 || month == 5 || month == 6)
            {
                return 2;
            }
            else if (month == 7 || month == 8 || month == 9)
            {
                return 3;
            }
            else if (month == 10 || month == 11 || month == 12)
            {
                return 4;
            }
            return 0;
        }
    }
}
