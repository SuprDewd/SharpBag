using System;

namespace SharpBag.BagLogging
{
    /// <summary>
    /// A class used for application logging.
    /// </summary>
    public sealed class BagLogger
    {
        /// <summary>
        /// A function used for logging.
        /// </summary>
        /// <param name="s">The output string that needs to be handled.</param>
        public delegate void LogFunction(string s);

        /// <summary>
        /// The logger.
        /// </summary>
        public LogFunction Logger { get; set; }
        /// <summary>
        /// Whether to prepend a timestamp on the output string or not.
        /// </summary>
        public bool Timestamp { get; set; }
        /// <summary>
        /// The format of the timestamp. See DateTime.ToString().
        /// </summary>
        public string Timeformat { get; set; }
        /// <summary>
        /// Whether the logger is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// A class used for application logging.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="timestamp"></param>
        public BagLogger(LogFunction logger, bool timestamp)
        {
            Logger = logger;
            Timestamp = timestamp;
            Timeformat = "dd.MM.yyyy HH:mm:ss: ";
            Enabled = true;
        }

        /// <summary>
        /// Log the specified string.
        /// </summary>
        /// <param name="s">The string to be logged.</param>
        public void Log(string s)
        {
            if (Enabled) Logger((Timestamp ? DateTime.Now.ToString(Timeformat) : "") + s);
        }

        /// <summary>
        /// Log the specified string if expression is true.
        /// </summary>
        /// <param name="expression">An expression.</param>
        /// <param name="s">The string to be logged.</param>
        public void LogIf(bool expression, string s)
        {
            if (expression) Log(s);
        }

        /// <summary>
        /// Log the specified string if expression is false.
        /// </summary>
        /// <param name="expression">An expression.</param>
        /// <param name="s">The string to be logged.</param>
        public void LogIfNot(bool expression, string s)
        {
            if (!expression) Log(s);
        }
    }
}
