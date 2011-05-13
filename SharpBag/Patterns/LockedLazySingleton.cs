using System;

namespace SharpBag.Patterns
{
	/// <summary>
	/// An eager singleton.
	/// </summary>
	/// <typeparam name="T">The type of the object, the singleton will wrap.</typeparam>
	public class LockedLazySingleton<T> : LazySingleton<T> where T : class, new()
	{
		/// <summary>
		/// The constructor.
		/// </summary>
		public LockedLazySingleton() { }

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="initializeAction">An action to initialize the object.</param>
		public LockedLazySingleton(Action<T> initializeAction) : base(initializeAction) { }

		/// <summary>
		/// Gets the object instance.
		/// </summary>
		/// <returns>The object instance.</returns>
		public override T GetInstance()
		{
			lock (this.UniqueInstance)
			{
				if (this.UniqueInstance == null)
				{
					this.UniqueInstance = new T();
					this.InitializeAction(this.UniqueInstance);
				}
			}

			return this.UniqueInstance;
		}
	}
}