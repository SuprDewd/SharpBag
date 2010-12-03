using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SharpBag.Strings;

namespace SharpBag.FK.MVC
{
    internal class FKActionMetadata
    {
        public MethodInfo Method { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Timed { get; set; }
        public bool Pause { get; set; }

        public override string ToString()
        {
            return this.Name + (this.Description != null ? ":\t" + this.Description : "");
        }

        public string ToString(int space)
        {
            if (this.Description == null) return this.Name;
            else return String.Format("{0,-" + (space + 1) + "} {1}", this.Name + ":", this.Description);
        }
    }
}