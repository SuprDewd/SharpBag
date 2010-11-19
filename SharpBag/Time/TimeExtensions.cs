using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SharpBag.Time
{
    public static class TimeExtensions
    {
        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime January(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime February(this int day, int year)
        {
            return new DateTime(year, 2, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime March(this int day, int year)
        {
            return new DateTime(year, 3, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime April(this int day, int year)
        {
            return new DateTime(year, 4, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime May(this int day, int year)
        {
            return new DateTime(year, 5, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime June(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime July(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime August(this int day, int year)
        {
            return new DateTime(year, 8, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime September(this int day, int year)
        {
            return new DateTime(year, 9, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime October(this int day, int year)
        {
            return new DateTime(year, 10, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime November(this int day, int year)
        {
            return new DateTime(year, 11, day);
        }

        /// <summary>
        /// Returns a new DateTime with the specified day, month and year.
        /// </summary>
        /// <param name="day">A day.</param>
        /// <param name="year">A year.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime December(this int day, int year)
        {
            return new DateTime(year, 12, day);
        }
        
        public static DateTime At(this DateTime datetime, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minute, second, millisecond);
        }

        public static TimeSpan Ticks(this long n)
        {
            return TimeSpan.FromTicks(n);
        }

        public static TimeSpan Milliseconds(this double n)
        {
            return TimeSpan.FromMilliseconds(n);
        }

        public static TimeSpan Seconds(this double n)
        {
            return TimeSpan.FromSeconds(n);
        }

        public static TimeSpan Minutes(this double n)
        {
            return TimeSpan.FromMinutes(n);
        }

        public static TimeSpan Hours(this double n)
        {
            return TimeSpan.FromHours(n);
        }

        public static TimeSpan Days(this double n)
        {
            return TimeSpan.FromDays(n);
        }

        public static TimeSpan Weeks(this double n)
        {
            return TimeSpan.FromDays(n * 7);
        }

        public static TimeSpan Milliseconds(this int n)
        {
            return TimeSpan.FromMilliseconds(n);
        }

        public static TimeSpan Seconds(this int n)
        {
            return TimeSpan.FromSeconds(n);
        }

        public static TimeSpan Minutes(this int n)
        {
            return TimeSpan.FromMinutes(n);
        }

        public static TimeSpan Hours(this int n)
        {
            return TimeSpan.FromHours(n);
        }

        public static TimeSpan Days(this int n)
        {
            return TimeSpan.FromDays(n);
        }

        public static TimeSpan Weeks(this int n)
        {
            return TimeSpan.FromDays(n * 7);
        }

        public static IEnumerable<DateTime> To(this DateTime from, DateTime to)
        {
            return from.To(to, 1.Days());
        }

        public static IEnumerable<DateTime> To(this DateTime from, DateTime to, TimeSpan step)
        {
            if (from < to)
            {
                while (from < to)
                {
                    yield return from;
                    from += step;
                }

                yield return to;
            }
            else
            {
                while (from > to)
                {
                    yield return from;
                    from -= step;
                }

                yield return to;
            }
        }

        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        public static int WeekOfYear(this DateTime datetime)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(datetime, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}