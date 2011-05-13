namespace SharpBag.Time
{
	/// <summary>
	/// A static class with utilites regarding dates and time.
	/// </summary>
	public static class TimeUtils
	{
		/// <summary>
		/// Determines whether the specified year is leap year or not.
		/// </summary>
		/// <param name="year">A year.</param>
		/// <returns>Whether the specified year is leap year or not.</returns>
		public static bool IsLeapYear(int year)
		{
			return year % 400 == 0 || year % 100 != 0 && year % 4 == 0;
		}
	}
}