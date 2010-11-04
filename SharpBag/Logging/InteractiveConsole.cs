using System;
using System.Linq;
using System.Threading;

namespace SharpBag.Logging
{
    /// <summary>
    /// A class used for managing the console window, so that the program can both read from the console and write into the console at the samt time.
    /// </summary>
    public class InteractiveConsole
    {
        /// <summary>
        /// A delegate that can be called when a command is entered.
        /// </summary>
        /// <param name="command">The command that was entered.</param>
        public delegate void CommandEnteredEvent(string command);
        /// <summary>
        /// An event that will be fired when a user enters a command into the console window.
        /// </summary>
        public event CommandEnteredEvent OnCommandEntered;
        /// <summary>
        /// The start of the command line.
        /// </summary>
        public string CommandStart { get; set; }
        /// <summary>
        /// Can be set to true to shut down the main reading thread and all readers.
        /// </summary>
        private bool Exit = false;
        /// <summary>
        /// Whether or not there is a reading going on.
        /// </summary>
        private bool Reading = false;
        /// <summary>
        /// The command that is currently being written into the console.
        /// </summary>
        private string Command = "";
        /// <summary>
        /// The main reader thread.
        /// </summary>
        private Thread ReaderThread = null;
        /// <summary>
        /// Valid input characters.
        /// </summary>
        public static Char[] ValidInput = new char[] { 'á', 'é', 'ú', 'ý', 'í', 'ó', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'ð', '\'', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'æ', '´', '+', '<', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', 'þ', '-', '_', '>', '"', '\\', '/', '!', '#', '$', '%', '&', '(', ')', '=', 'ö', '{', '}', '[', ']', '°', ' ', '*', '~', '?' };

        /// <summary>
        /// The main constructor.
        /// </summary>
        /// <param name="commandStart">The start of the command line.</param>
        public InteractiveConsole(string commandStart = "> ")
        {
            this.CommandStart = commandStart;
            for (int i = 0; i < Console.BufferHeight; i++)
            {
                Console.WriteLine();
            }

            Console.Write(commandStart);
        }

        /// <summary>
        /// The main deconstructor.
        /// </summary>
        ~InteractiveConsole()
        {
            this.Stop();
        }

        /// <summary>
        /// Stop all readers and reader threads.
        /// Note: This should always be called before application shutdown to make sure all threads are terminated.
        /// </summary>
        public void Stop()
        {
            this.Exit = true;

            if (this.ReaderThread != null)
            {
                this.ReaderThread.Abort();
            }
        }

        /// <summary>
        /// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
        /// </summary>
        public void ReadCommandAsync()
        {
            this.Exit = false;
            if (!this.Reading)
            {
                this.ReaderThread = new Thread(new ThreadStart(this._ReadCommandAsync));
                this.ReaderThread.Start();
            }
        }

        /// <summary>
        /// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
        /// </summary>
        private void _ReadCommandAsync()
        {
            while (!this.Exit)
            {
                this.OnCommandEntered(this.ReadCommand());
            }
        }

        /// <summary>
        /// Reads a command and returns it as a string.
        /// Note: This method will not return anything until the command is entered. Use ReadCommandAsync to read commands asynchronously.
        /// </summary>
        /// <returns></returns>
        public string ReadCommand()
        {
            if (this.Reading) return null;

            this.Exit = false;
            this.Reading = true;
            string command = "";
            while (true)
            {
                if (this.Exit)
                {
                    this.Reading = false;
                    return null;
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo c = Console.ReadKey();
                    if (c.Key == ConsoleKey.Enter)
                    {
                        command = this.Command;
                        this.Command = "";
                        this.WriteLine(null);
                        break;
                    }
                    else if (ValidInput.Contains<char>(c.KeyChar.ToLower()))
                    {
                        if (c.Key == ConsoleKey.Backspace)
                        {
                            try
                            {
                                this.Command = this.Command.Substring(0, this.Command.Length - 1);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            this.Command += c.KeyChar;
                        }

                        this.WriteLine(null);
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }

            this.Reading = false;
            return command;
        }

        /// <summary>
        /// Writes a string to the console.
        /// Note: This should be used instead of Console.WriteLine and Console.Write.
        /// </summary>
        /// <param name="o"></param>
        public void WriteLine(object o)
        {
            string s = (o == null ? null : o.ToString());
            Console.CursorLeft = 0;
            if (s != null)
            {
                Console.Write(s);
                for (int i = 1; i < Console.BufferWidth - s.Length; i++)
                {
                    Console.Write(" ");
                }
            }

            Console.Write((s == null ? "" : "\n") + this.CommandStart + this.Command);
            int k = Console.BufferWidth - this.Command.Length - 3;
            for (int i = 0; i < k; i++)
            {
                Console.Write(" ");
            }

            try
            {
                Console.CursorLeft -= k;
            }
            catch { }
        }
    }
}