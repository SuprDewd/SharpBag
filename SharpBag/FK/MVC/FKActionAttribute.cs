using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.FK.MVC
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class FKActionAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }

        public FKActionAttribute(string name) { this.Name = name; this.Finished = true; }
    }
}