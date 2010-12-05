using System;

namespace SharpBag.FK.MVC
{
    /// <summary>
    /// An MVC view.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class FKViewAttribute : Attribute { }
}