using System;
using System.Reflection;

namespace SharpBag.Misc
{
    /// <summary>
    /// A problem.
    /// </summary>
    public class ProblemMetadata
    {
        /// <summary>
        /// The method.
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether to time the problem
        /// </summary>
        public Nullable<bool> Time { get; set; }

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
        /// <param name="problem">The problem.</param>
        /// <param name="method">The method.</param>
        internal ProblemMetadata(ProblemAttribute problem, MethodInfo method)
        {
            this.Method = method;
            this.Title = problem.Title ?? "";
            this.Description = problem.Description ?? "";
            this.Time = problem.InternalTime;
            this.Finished = problem.Finished;
            this.Pause = problem.Pause;
            this.Start = problem.Start;
        }

        internal string ToString(int space)
        {
            return this.Description == null ? this.Title : String.Format("{0,-" + (space + 1) + "}: {2}{1}", this.Title, this.Finished ? "" : " (Not Finished)", this.Description);
        }
    }
}