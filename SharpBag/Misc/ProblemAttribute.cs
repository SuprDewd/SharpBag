using System;

namespace SharpBag.Misc
{
    /// <summary>
    /// A problem.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ProblemAttribute : Attribute
    {
        /// <summary>
        /// The title of the problem.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The description of the problem.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether to time the problem.
        /// </summary>
        public bool Time { set { this.InternalTime = value; } get { return this.InternalTime.HasValue && this.InternalTime.Value; } }

        internal Nullable<bool> InternalTime { get; set; }

        /// <summary>
        /// Whether the problem is finished.
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// Whether to pause after the problem has finished running.
        /// </summary>
        public bool Pause { get; set; }

        /// <summary>
        /// Whether to run the problem on start.
        /// </summary>
        public bool Start { get; set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="title">The title of the problem.</param>
        public ProblemAttribute(string title)
        {
            this.Title = title;
            this.Pause = this.Finished = true;
        }
    }
}