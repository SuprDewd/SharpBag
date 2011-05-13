using System;

namespace SharpBag.Patterns
{
	/// <summary>
	/// A singleton.
	/// </summary>
	/// <typeparam name="T">The type of the object, the singleton will wrap.</typeparam>
	public abstract class Singleton<T> where T : class, new()
	{
		/// <summary>
		/// The unique instance of the object.
		/// </summary>
		protected T UniqueInstance { get; set; }

		/// <summary>
		/// An action to initialize the object.
		/// </summary>
		protected Action<T> InitializeAction { get; set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		protected Singleton() { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="initializeAction">An action to initialize the object.</param>
		protected Singleton(Action<T> initializeAction)
		{
			this.InitializeAction = initializeAction;
		}

		/// <summary>
		/// Gets the object instance.
		/// </summary>
		/// <returns>The object instance.</returns>
		public abstract T GetInstance();
	}
}