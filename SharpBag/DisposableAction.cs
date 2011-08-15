using System;

using System.Diagnostics.Contracts;

namespace SharpBag
{
    #region Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx

    /// <summary>
    /// Makes an action disposable.
    /// </summary>
    public class DisposableAction : IDisposable
    {
        private readonly Action DisposeAction;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="action">The action to execute when the current instance is disposed.</param>
        public DisposableAction(Action action)
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

    #endregion Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx
}