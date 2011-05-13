using System;
using System.Linq;
using System.Threading;
using SharpBag.Strings;

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
		private bool _Exit;
		/// <summary>
		/// Whether or not there is a reading going on.
		/// </summary>
		private bool _Reading;
		/// <summary>
		/// The command that is currently being written into the console.
		/// </summary>
		private string _Command = "";
		/// <summary>
		/// The main reader thread.
		/// </summary>
		private Thread _ReaderThread;
		/// <summary>
		/// Valid input characters.
		/// </summary>
		private static readonly Char[] ValidInput = new[] { 'á', 'é', 'ú', 'ý', 'í', 'ó', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'ð', '\'', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'æ', '´', '+', '<', 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', 'þ', '-', '_', '>', '"', '\\', '/', '!', '#', '$', '%', '&', '(', ')', '=', 'ö', '{', '}', '[', ']', '°', ' ', '*', '~', '?' };

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
		/// This should always be called before application shutdown to make sure all threads are terminated.
		/// </summary>
		public void Stop()
		{
			this._Exit = true;

			if (this._ReaderThread != null)
			{
				this._ReaderThread.Abort();
			}
		}

		/// <summary>
		/// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
		/// </summary>
		public void ReadCommandAsync()
		{
			this._Exit = false;
			if (!this._Reading)
			{
				this._ReaderThread = new Thread(this.ReadCommandListener);
				this._ReaderThread.Start();
			}
		}

		/// <summary>
		/// Reads commands asynchronously and fires the OnCommandEntered event when a command has been entered.
		/// </summary>
		private void ReadCommandListener()
		{
			while (!this._Exit) this.OnCommandEntered(this.ReadCommand());
		}

		/// <summary>
		/// Reads a command and returns it as a string.
		/// This method will not return anything until the command is entered. Use ReadCommandAsync to read commands asynchronously.
		/// </summary>
		/// <returns>The command.</returns>
		public string ReadCommand()
		{
			if (this._Reading) return null;

			this._Exit = false;
			this._Reading = true;
			string command;
			while (true)
			{
				if (this._Exit)
				{
					this._Reading = false;
					return null;
				}

				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo c = Console.ReadKey();
					if (c.Key == ConsoleKey.Enter)
					{
						command = this._Command;
						this._Command = "";
						this.WriteLine(null);
						break;
					}
					else if (c.Key == ConsoleKey.Backspace)
					{
						try
						{
							this._Command = this._Command.Substring(0, this._Command.Length - 1);
						}
						catch { }

						this.WriteLine(null);
					}
					else if (ValidInput.Contains(c.KeyChar.ToLower()))
					{
						this._Command += c.KeyChar;
						this.WriteLine(null);
					}
				}
				else
				{
					Thread.Sleep(10);
				}
			}

			this._Reading = false;
			return command;
		}

		/// <summary>
		/// Writes a string to the console.
		/// This should be used instead of Console.WriteLine and Console.Write.
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

			Console.Write((s == null ? "" : "\n") + this.CommandStart + this._Command);
			int k = Console.BufferWidth - this._Command.Length - 3;
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