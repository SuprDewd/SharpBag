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
    public abstract class FKController
    {
        public string Title { get; set; }

        public FKModel Model { get; set; }

        public char VerticalChar { get; set; }
        public char HorizontalChar { get; set; }

        private IEnumerable<FKActionMetadata> Actions
        {
            get
            {
                return (from a in this.GetType().GetMethods()
                        where a.IsPublic && Attribute.IsDefined(a, typeof(FKActionAttribute)) && !a.GetParameters().Any()
                        let attr = Attribute.GetCustomAttribute(a, typeof(FKActionAttribute)) as FKActionAttribute
                        select new FKActionMetadata { Method = a, Name = attr.Name, Description = attr.Description + (!attr.Finished ? " (unfinished)" : "") }).OrderBy(a => a.Name, new AlphaNumberComparer(AlphaNumberSettings.Trailing));
            }
        }

        public FKController(FKModel model, string title = null, char verticalChar = '-', char horizontalChar = '|', string[] args = null)
        {
            this.Model = model;
            this.Title = title;
            this.VerticalChar = verticalChar;
            this.HorizontalChar = horizontalChar;

            try
            {
                if (args != null && args.Any())
                {
                    bool endless = args.Skip(1).Any() && args[1].StartsWithIgnoreCase("T");

                    while (true)
                    {
                        this.ExecuteAction(args.First(), true, true);

                        if (endless) continue;

                        this.Exit();
                    }
                }
            }
            catch { }
        }

        public void ListActions()
        {
            if (this.Title != null) this.WriteHeader(this.Title);

            foreach (var a in this.Actions)
            {
                Console.WriteLine(a.ToString());
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

        public void ExecuteAction(string actionName, bool pause = false, bool header = false)
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

            action.Method.Invoke(this, new object[] { });

            this.PostActionExecute();

            if (pause) Console.ReadLine();
        }

        [FKAction("All", Description = "Run all the actions.")]
        public void All()
        {
            foreach (var item in this.Actions.Where(a => !a.Name.IsIn("All", "Exit")))
            {
                //Console.Clear();
                this.ExecuteAction(item.Name, true, true);
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                this.ListActions();
                Console.Write("Run: ");
                this.ExecuteAction(Console.ReadLine(), true, true);
            }
        }

        [FKAction("Exit", Description = "Exit the program.")]
        public void Exit()
        {
            Environment.Exit(0);
        }

        public virtual void PreActionExecute() { }
        public virtual void PostActionExecute() { }

        public void SimpleView(params object[] objs)
        {
            foreach (object o in objs)
            {
                Console.WriteLine(o);
            }
        }

        public void SimpleView(Dictionary<string, object> objs, string between = ": ")
        {
            foreach (var item in objs)
            {
                Console.Write(item.Key);
                Console.Write(between);
                Console.WriteLine(item.Value);
            }
        }
    }
}