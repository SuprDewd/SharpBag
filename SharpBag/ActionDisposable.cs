using System;

#if DOTNET4
using System.Diagnostics.Contracts;
#endif

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
#if DOTNET4
            Contract.Requires(action != null);
#endif
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