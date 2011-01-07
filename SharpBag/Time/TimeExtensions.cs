using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using SharpBag.Math;

namespace SharpBag.Time
{
    /// <summary>
    /// A static class with extension methods for date and time.
    /// </summary>
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
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
            Contract.Requires(day.IsBetweenOrEqualTo(1, 31));
            Contract.Requires(year.IsBetweenOrEqualTo(0, 9999));
            return new DateTime(year, 12, day);
        }

        /// <summary>
        /// Sets the current instance to the specified hour, minute, second and millisecond.
        /// </summary>
        /// <param name="datetime">The current instance.</param>
        /// <param name="hour">The specified hour.</param>
        /// <param name="minute">The specified minute.</param>
        /// <param name="second">The specified second.</param>
        /// <param name="millisecond">The specified millisecond.</param>
        /// <returns>The new DateTime.</returns>
        public static DateTime At(this DateTime datetime, int hour, int minute = 0, int second = 0, int millisecond = 0)
        {
            Contract.Requires(hour.IsBetweenOrEqualTo(0, 23));
            Contract.Requires(minute.IsBetweenOrEqualTo(0, 59));
            Contract.Requires(second.IsBetweenOrEqualTo(0, 59));
            Contract.Requires(minute.IsBetweenOrEqualTo(0, 999));
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as ticks.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Ticks(this long n)
        {
            return TimeSpan.FromTicks(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as milliseconds.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Milliseconds(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromMilliseconds(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as seconds.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Seconds(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromSeconds(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as minutes.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Minutes(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromMinutes(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as hours.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Hours(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromHours(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as days.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Days(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromDays(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as weeks.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Weeks(this double n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromDays(n * 7);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as milliseconds.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Milliseconds(this int n)
        {
            Contract.Requires(!Double.IsNaN(n));
            return TimeSpan.FromMilliseconds(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as seconds.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Seconds(this int n)
        {
            return TimeSpan.FromSeconds(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as minutes.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Minutes(this int n)
        {
            return TimeSpan.FromMinutes(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as hours.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Hours(this int n)
        {
            return TimeSpan.FromHours(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as days.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Days(this int n)
        {
            return TimeSpan.FromDays(n);
        }

        /// <summary>
        /// Gets a TimeSpan with current instance as weeks.
        /// </summary>
        /// <param name="n">The current instance.</param>
        /// <returns>The TimeSpan.</returns>
        public static TimeSpan Weeks(this int n)
        {
            return TimeSpan.FromDays(n * 7);
        }

        #region To overloads

        /// <summary>
        /// Returns a collection of DateTimes with a one date interval.
        /// </summary>
        /// <param name="from">The current instance, or the DateTime to start at.</param>
        /// <param name="to">The DateTime to end at.</param>
        /// <returns>A collection of DateTimes with a one date interval.</returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to)
        {
            return from.To(to, 1.Days());
        }

        /// <summary>
        /// Returns a collection of DateTimes with a one date interval.
        /// </summary>
        /// <param name="from">The current instance, or the DateTime to start at.</param>
        /// <param name="to">The DateTime to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>A collection of DateTimes with a one date interval.</returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to, TimeSpan step)
        {
            if (from < to)
            {
                do
                {
                    yield return from;
                    from += step;
                }
                while (from <= to);
            }
            else if (from > to)
            {
                do
                {
                    yield return from;
                    from -= step;
                } while (from >= to);
            }
            else yield return from;
        }

        #endregion To overloads

        /// <summary>
        /// Returns the time that has elapsed since the time of the current instance.
        /// </summary>
        /// <param name="datetime">The current instance.</param>
        /// <returns>The time that has elapsed since the time of the current instance.</returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }

        /// <summary>
        /// Returns what week of the year, the current instance is on.
        /// </summary>
        /// <param name="datetime">The current instance.</param>
        /// <returns>What week of the year, the current instance is on.</returns>
        public static int WeekOfYear(this DateTime datetime)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(datetime, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}