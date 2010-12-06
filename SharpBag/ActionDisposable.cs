using System;
using System.Diagnostics.Contracts;

namespace SharpBag
{
    /// <summary>
    /// Makes an action disposable.
    /// </summary>
    public class ActionDisposable : IDisposable
    {
        private readonly Action DisposeAction;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="action">The action to execute when the current instance is disposed.</param>
        public ActionDisposable(Action action)
        {
            Contract.Requires(action != null);
            this.DisposeAction = action;
        }

        /// <summary>
        /// The disposer which executes the dispose action.
        /// </summary>
        public void Dispose()
        {
            this.DisposeAction();
        }
    }
}