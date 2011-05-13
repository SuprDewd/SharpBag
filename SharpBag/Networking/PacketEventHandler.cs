using System;
using System.Collections.Generic;

namespace SharpBag.Networking
{
	/// <summary>
	/// A packet-event handler.
	/// </summary>
	public abstract class PacketEventHandler
	{
		/// <summary>
		/// Fire the specified event on the other side.
		/// </summary>
		/// <typeparam name="T">The type of data to send.</typeparam>
		/// <param name="eventID">The ID of the event.</param>
		/// <param name="obj">The data to send.</param>
		/// <returns>The current instance (for chaining).</returns>
		public abstract PacketEventHandler Fire<T>(int eventID, T obj);

		/// <summary>
		/// Fire the specified event on the server.
		/// </summary>
		/// <param name="eventID">The ID of the event.</param>
		/// <returns>The current instance (for chaining).</returns>
		public abstract PacketEventHandler Fire(int eventID);

		/// <summary>
		/// The events.
		/// </summary>
		internal Dictionary<int, Action<object, PacketEventArgs>> Events = new Dictionary<int, Action<object, PacketEventArgs>>();

		/// <summary>
		/// Subscribe to the specified event.
		/// </summary>
		/// <param name="eventID">The ID of the event.</param>
		/// <param name="action">The action to call when the event occurs.</param>
		/// <returns>The current instance (for chaining).</returns>
		public PacketEventHandler Subscribe(int eventID, Action<object, PacketEventArgs> action)
		{
			this.Events.Add(eventID, action);
			return this;
		}

		/// <summary>
		/// Subscribe to the specified event.
		/// </summary>
		/// <typeparam name="T">The type of the data.</typeparam>
		/// <param name="eventID">The ID of the event.</param>
		/// <param name="action">The action to call when the event occurs.</param>
		/// <returns>The current instance (for chaining).</returns>
		public PacketEventHandler Subscribe<T>(int eventID, Action<T, PacketEventArgs> action)
		{
			this.Events.Add(eventID, (o, hea) => action((T)o, hea));
			return this;
		}

		/// <summary>
		/// Unsubscribe from the specified event.
		/// </summary>
		/// <param name="eventID">The ID of the event.</param>
		/// <returns>The current instance (for chaining).</returns>
		public PacketEventHandler Unsubscribe(int eventID)
		{
			this.Events.Remove(eventID);
			return this;
		}
	}
}