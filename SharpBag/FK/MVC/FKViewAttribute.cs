using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.FK.MVC
{
    /// <summary>
    /// An MVC view.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class FKViewAttribute : Attribute { }
}
