using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpBag.Misc
{
    internal class ProblemMetadata
    {
        public MethodInfo Method { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Nullable<bool> Time { get; set; }

        public bool Finished { get; set; }

        public bool Pause { get; set; }

        public bool Start { get; set; }

        public ProblemMetadata(ProblemAttribute problem, MethodInfo method)
        {
            this.Method = method;
            this.Title = problem.Title ?? "";
            this.Description = problem.Description ?? "";
            this.Time = problem.Time;
            this.Finished = problem.Finished;
            this.Pause = problem.Pause;
            this.Start = problem.Start;
        }

        public string ToString(int space)
        {
            return this.Description == null ? this.Title : String.Format("{0,-" + (space + 1) + "}: {2}{1}", this.Title, this.Finished ? "" : " (Not Finished)", this.Description);
        }
    }
}