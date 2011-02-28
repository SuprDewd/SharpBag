using System;

#if DOTNET4
using System.Diagnostics.Contracts;
#endif

namespace SharpBag
{
    #region Yet Another Language Geek - http://blogs.msdn.com/b/wesdyer/archive/2007/02/23/linq-to-ascii-art.aspx
    
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
	
	#endregion
}