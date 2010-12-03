using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBag.FK.MVC
{
    /// <summary>
    /// A controller action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class FKActionAttribute : Attribute
    {
        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A description of the action.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Whether the action is finished.
        /// </summary>
        public bool Finished { get; set; }
        /// <summary>
        /// Whether the action should be timed.
        /// </summary>
        public bool Timed { get; set; }
        /// <summary>
        /// Whether the program should be paused when the action finishes.
        /// </summary>
        public bool Pause { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="name">The name of the action.</param>
        public FKActionAttribute(string name) { this.Name = name; this.Finished = true; this.Pause = true; }
    }
}