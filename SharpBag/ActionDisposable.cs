using System;

namespace SharpBag
{
    /// <summary>
    /// Makes an action disposable.
    /// </summary>
    public class ActionDisposable : IDisposable
    {
        Action action;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="action">The action to execute when the current instance is disposed.</param>
        public ActionDisposable(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// The disposer which executes the dispose action.
        /// </summary>
        void IDisposable.Dispose()
        {
            action();
        }
    }
}