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
            Contract.Requires(hour >= 0 && hour <= 23);
            Contract.Requires(minute >= 0 && hour <= 59);
            Contract.Requires(second >= 0 && second <= 59);
            Contract.Requires(millisecond >= 0 && millisecond <= 999);

            return new DateTime(datetime.Year, datetime.Month, datetime.Day, hour, minute, second, millisecond);
        }

        #region To

        /// <summary>
        /// Returns a collection of DateTimes with a one date interval.
        /// </summary>
        /// <param name="from">The current instance, or the DateTime to start at.</param>
        /// <param name="to">The DateTime to end at.</param>
        /// <returns>A collection of DateTimes with a one date interval.</returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to)
        {
            return from.To(to, TimeSpan.FromDays(1));
        }

        /// <summary>
        /// Returns a collection of DateTimes with the specified interval.
        /// </summary>
        /// <param name="from">The current instance, or the DateTime to start at.</param>
        /// <param name="to">The DateTime to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>A collection of DateTimes with a one date interval.</returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to, TimeSpan step)
        {
            if (from <= to)
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
        }

        /// <summary>
        /// Returns a collection of DateTimes with a the specified interval.
        /// </summary>
        /// <param name="from">The current instance, or the DateTime to start at.</param>
        /// <param name="to">The DateTime to end at.</param>
        /// <param name="step">The step to take on each iteration.</param>
        /// <returns>A collection of DateTimes with a one date interval.</returns>
        public static IEnumerable<DateTime> To(this DateTime from, DateTime to, Func<DateTime, DateTime> step)
        {
            Contract.Requires(step != null);

            if (from <= to)
            {
                do
                {
                    yield return from;
                    from = step(from);
                }
                while (from <= to);
            }
            else if (from > to)
            {
                do
                {
                    yield return from;
                    from = step(from);
                } while (from >= to);
            }
        }

        #endregion To

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