using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.Patterns
{
    /// <summary>
    /// An eager singleton.
    /// </summary>
    /// <typeparam name="T">The type of the object, the singleton will wrap.</typeparam>
    public class EagerSingleton<T> : Singleton<T> where T : class, new()
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public EagerSingleton() : this(null) { }
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="initializeAction">An action to initialize the object.</param>
        public EagerSingleton(Action<T> initializeAction) : base(initializeAction) { this.UniqueInstance = new T(); this.InitializeAction(this.UniqueInstance); }

        /// <summary>
        /// Gets the object instance.
        /// </summary>
        /// <returns>The object instance.</returns>
        public override T GetInstance()
        {
            return this.UniqueInstance;
        }
    }
}