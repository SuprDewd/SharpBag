using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SharpBag.Strings;
using SharpBag;
using SharpBag.Comparers;

namespace SharpBag.FK.MVC
{
    /// <summary>
    /// An MVC controller.
    /// </summary>
    public abstract class FKController
    {
        /// <summary>
        /// The title of the controller.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The model for the controller.
        /// </summary>
        public FKModel Model { get; set; }

        /// <summary>
        /// A char for the vertical wall of the title.
        /// </summary>
        public char VerticalChar { get; set; }
        /// <summary>
        /// A char for the horizontal wall of the title.
        /// </summary>
        public char HorizontalChar { get; set; }

        /// <summary>
        /// Whether to time all actions.
        /// </summary>
        public bool TimeAll { get; set; }

        private IEnumerable<FKActionMetadata> Actions
        {
            get
            {
                return (from a in this.GetType().GetMethods()
                        where a.IsPublic && Attribute.IsDefined(a, typeof(FKActionAttribute)) && !a.GetParameters().Any()
                        let attr = Attribute.GetCustomAttribute(a, typeof(FKActionAttribute)) as FKActionAttribute
                        select new FKActionMetadata { Method = a, Name = attr.Name, Description = attr.Description + (!attr.Finished ? " (unfinished)" : ""), Timed = attr.Timed, Pause = attr.Pause }).OrderBy(a => a.Name, new AlphaNumberComparer(AlphaNumberSettings.Leading));
            }
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="model">The model for the controller.</param>
        /// <param name="title">The title of the controller.</param>
        /// <param name="timeAll">Whether to time all actions.</param>
        /// <param name="verticalChar">A char for the vertical wall of the title.</param>
        /// <param name="horizontalChar">A char for the horizontal wall of the title.</param>
        /// <param name="args">Arguments, or settings, for the controller.</param>
        public FKController(FKModel model, string title = null, bool timeAll = false, char verticalChar = '-', char horizontalChar = '|', string[] args = null)
        {
            this.Model = model;
            this.Title = title;
            this.VerticalChar = verticalChar;
            this.HorizontalChar = horizontalChar;
            this.TimeAll = timeAll;

            try
            {
                if (args != null && args.Any())
                {
                    bool endless = args.Skip(1).Any() && args[1].StartsWithIgnoreCase("T");

                    while (true)
                    {
                        this.ExecuteAction(args.First(), true);

                        if (endless) continue;

                        this.Exit();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// List all the actions in the controller.
        /// </summary>
        public void ListActions()
        {
            if (this.Title != null) this.WriteHeader(this.Title);

            FKActionMetadata[] actions = this.Actions.ToArray();
            int longestName = actions.Max(a => a.Name.Length);

            foreach (var a in actions)
            {
                Console.WriteLine(a.ToString(longestName));
            }

            Console.WriteLine();
        }

        private void WriteHeader(string header)
        {
            var lines = header.Lines();
            int length = lines.Select(l => l.Length).Max();
            string verticalString = new String(this.VerticalChar, length + 4);

            Console.WriteLine(verticalString);

            foreach (string line in lines)
            {
                Console.Write(this.HorizontalChar);
                Console.Write(' ');

                Console.Write(line);

                Console.Write(new String(' ', length - line.Length + 1));
                Console.WriteLine(this.HorizontalChar);
            }

            Console.WriteLine(verticalString);

            Console.WriteLine();
        }

        /// <summary>
        /// Execute the specified action.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="header">Whether to display a header.</param>
        public void ExecuteAction(string actionName, bool header = false)
        {
            var action = (from a in this.Actions
                          let distance = a.Name.DistanceTo(actionName, false)
                          where distance <= 3
                          orderby distance
                          select a).FirstOrDefault();

            if (action == null || actionName.Trim() == "") action = this.Actions.FirstOrDefault(a => a.Name == "Exit");
            if (action == null) return;

            this.PreActionExecute();

            if (header) this.WriteHeader(action.Name + (action.Description != null ? "\n" + action.Description : ""));

            if (action.Timed || this.TimeAll)
            {
                long time = Utils.ExecutionTime(() => action.Method.Invoke(this, new object[] { }));

                Console.WriteLine();
                Console.Write("Time: ");
                Console.WriteLine(time);
            }
            else
            {
                action.Method.Invoke(this, new object[] { });
            }

            this.PostActionExecute();

            if (action.Pause) Console.ReadLine();
        }

        /// <summary>
        /// An action that executes all the other actions.
        /// </summary>
        [FKAction("All", Description = "Run all the actions.", Pause = false)]
        public void All()
        {
            foreach (var item in this.Actions)
            {
                if (item.Name.IsIn("All", "Exit")) continue;
                this.ExecuteAction(item.Name, true);
            }
        }

        /// <summary>
        /// Run the controller in an interactive mode.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                Console.Clear();

                this.ListActions();
                Console.Write("Run: ");
                this.ExecuteAction(Console.ReadLine(), true);
            }
        }

        /// <summary>
        /// An action that exits the program.
        /// </summary>
        [FKAction("Exit", Description = "Exit the program.", Pause = false)]
        public void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// A hook that is fired before an action is executed.
        /// </summary>
        public virtual void PreActionExecute() { }
        /// <summary>
        /// A hook that is fired after an action is executed.
        /// </summary>
        public virtual void PostActionExecute() { }

        /// <summary>
        /// A simple view.
        /// </summary>
        /// <param name="objs">A collection of objects.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void SimpleView(IEnumerable<object> objs, bool space = true)
        {
            this.SimpleView(space, objs.ToArray());
        }

        /// <summary>
        /// A simple view.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="objs">A collection of objects.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void SimpleView<T>(IEnumerable<T> objs, bool space = true)
        {
            this.SimpleView(space, objs.ToArray());
        }

        /// <summary>
        /// A simple view.
        /// </summary>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        /// <param name="objs">A collection of objects.</param>
        public void SimpleView(bool space = true, params object[] objs)
        {
            if (space) Console.WriteLine();

            foreach (object o in objs)
            {
                Console.WriteLine(o);
            }
        }

        /// <summary>
        /// A simple view.
        /// </summary>
        /// <param name="objs">A dictionary with keys and values.</param>
        /// <param name="between">A string to put in between the key and the value.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void SimpleView(Dictionary<string, object> objs, string between = ": ", bool space = true)
        {
            if (space) Console.WriteLine();

            foreach (var item in objs)
            {
                Console.Write(item.Key);
                Console.Write(between);
                Console.WriteLine(item.Value);
            }
        }

        /// <summary>
        /// A simple view.
        /// </summary>
        /// <param name="s">A string to be printed.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void SimpleView(string s, bool space = true)
        {
            if (space) Console.WriteLine();
            Console.WriteLine(s);
        }

        /// <summary>
        /// A boolean view.
        /// </summary>
        /// <param name="condition">An expression.</param>
        /// <param name="trueString">A string that will be printed if the expression is true.</param>
        /// <param name="falseString">A string that will be printed if the expression is false.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void BoolView(bool condition, string trueString, string falseString, bool space = true)
        {
            if (space) Console.WriteLine();
            Console.WriteLine(condition ? trueString : falseString);
        }

        /// <summary>
        /// A boolean view.
        /// </summary>
        /// <param name="condition">An expression.</param>
        /// <param name="start">The first part of the string.</param>
        /// <param name="not">A string that will be printed between start and end if the expression is false.</param>
        /// <param name="end">The last part of the string.</param>
        /// <param name="space">Whether to prepend a space before displaying the view.</param>
        public void BoolView(bool condition, string start, string not, string end, bool space = true)
        {
            if (space) Console.WriteLine();

            Console.Write(start);
            Console.Write(" ");

            if (!condition)
            {
                Console.Write(not);
                Console.Write(" ");
            }

            Console.WriteLine(end);
        }
    }
}