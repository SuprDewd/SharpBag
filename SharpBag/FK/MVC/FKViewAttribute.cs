using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.FK.MVC
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class FKViewAttribute : Attribute
    {
        public FKViewAttribute() { }
    }
}
