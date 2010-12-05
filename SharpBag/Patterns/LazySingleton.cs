using System;

namespace SharpBag.Patterns
{
    /// <summary>
    /// A lazy singleton.
    /// </summary>
    /// <typeparam name="T">The type of the object, the singleton will wrap.</typeparam>
    public class LazySingleton<T> : Singleton<T> where T : class, new()
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public LazySingleton() { }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="initializeAction">An action to initialize the object.</param>
        public LazySingleton(Action<T> initializeAction) : base(initializeAction) { }

        /// <summary>
        /// Gets the object instance.
        /// </summary>
        /// <returns>The object instance.</returns>
        public override T GetInstance()
        {
            if (this.UniqueInstance == null)
            {
                this.UniqueInstance = new T();
                this.InitializeAction(this.UniqueInstance);
            }

            return this.UniqueInstance;
        }
    }
}