using System;

namespace SharpBag.Networking
{
	/// <summary>
	/// A connection.
	/// </summary>
	public interface IConnection : IDisposable
	{
		/// <summary>
		/// Open the connection.
		/// </summary>
		void Close();

		/// <summary>
		/// Close the connection.
		/// </summary>
		void Open();
	}
}