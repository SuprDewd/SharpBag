using System;
using System.Reflection;

namespace SharpBag.FK.MVC
{
    internal class FKActionMetadata
    {
        public MethodInfo Method { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Timed { get; set; }
		public bool Pause { get; set; }
		public bool Start { get; set; }

        public override string ToString()
        {
            return this.Name + (this.Description != null ? ":\t" + this.Description : "");
        }

        public string ToString(int space)
        {
            return this.Description == null ? this.Name : String.Format("{0,-" + (space + 1) + "} {1}", this.Name + ":", this.Description);
        }
    }
}