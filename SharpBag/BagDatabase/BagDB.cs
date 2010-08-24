using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;

namespace SharpBag.BagDatabase
{
    /// <summary>
    /// A class for working with MySQL databases.
    /// </summary>
    public class BagDB
    {
        /// <summary>
        /// The server to connect to.
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// The default schema.
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// The username used to connect.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The password used to connect.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// The time before the MySQL connection times out.
        /// </summary>
        public int CommandTimeout { get; set; }
        /// <summary>
        /// The state of the MySQL connection.
        /// </summary>
        public ConnectionState State { get { return this.Connection.State; } }

        /// <summary>
        /// The MySQL connection.
        /// </summary>
        public MySqlConnection Connection { get; set; }

        /// <summary>
        /// The main BagDB constructor.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        /// <param name="schema">The default schema.</param>
        /// <param name="username">The username used to connect.</param>
        /// <param name="password">The password used to connect.</param>
        public BagDB(string server, string schema, string username, string password)
        {
            this.CommandTimeout = 120;
            this.Server = server;
            this.Schema = schema;
            this.Username = username;
            this.Password = password;
            this.Connect();
        }

        /// <summary>
        /// Connects, or reconnects, to the MySQL database.
        /// </summary>
        public void Connect()
        {
            if (this.Connection == null) this.Connection = new MySqlConnection();
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.ConnectionString = "SERVER=" + this.Server + ";DATABASE=" + this.Schema + ";UID=" + this.Username + ";PWD=" + this.Password + ";";
                this.Connection.Open();
            }
        }

        /// <summary>
        /// Send a query to the MySQL database.
        /// </summary>
        /// <param name="q">The query string.</param>
        /// <returns>A DataTable object with the results from the query.</returns>
        public DataTable Query(string q)
        {
            Monitor.Enter(this.Connection);
            this.Connect();
            DataTable dt = null;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                {
                    cmd.CommandTimeout = this.CommandTimeout;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Dispose();
                }
            }
            finally
            {
                Monitor.Exit(this.Connection);
            }
            return dt;
        }

        /// <summary>
        /// Send a query to the MySQL database and only return the first column of the first row.
        /// </summary>
        /// <param name="q">The query string.</param>
        /// <returns>The first column of the first row.</returns>
        public object QuerySingle(string q)
        {
            this.Connect();
            Monitor.Enter(this.Connection);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                {
                    cmd.CommandTimeout = this.CommandTimeout;
                    return cmd.ExecuteScalar();
                }
            }
            finally
            {
                Monitor.Exit(this.Connection);
            }
        }

        /// <summary>
        /// Send a query to the MySQL database and only return the first column of the first row casted to T.
        /// </summary>
        /// <typeparam name="T">The type of the first column.</typeparam>
        /// <param name="q">The query string.</param>
        /// <returns>The first column of the first row casted to T.</returns>
        public T QuerySingle<T>(string q)
        {
            try
            {
                object o = this.QuerySingle(q);
                if (o.GetType() == typeof(DBNull)) return default(T);
                else return (T)o;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Executes a query on the MySQL database and returns how many rows were affected.
        /// </summary>
        /// <param name="q">The query to execute.</param>
        /// <returns>How many rows were affected.</returns>
        public int Execute(string q)
        {
            this.Connect();
            Monitor.Enter(this.Connection);
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(q, this.Connection))
                {
                    cmd.CommandTimeout = this.CommandTimeout;
                    return cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                Monitor.Exit(this.Connection);
            }
        }

        /// <summary>
        /// Kills the current thread and then closes the MySQL connection.
        /// </summary>
        public void Close()
        {
            if (this.Connection.State != ConnectionState.Closed)
            {
                try
                {
                    this.Execute("KILL " + this.Connection.ServerThread + ";");
                }
                catch { }
                this.Connection.Close();
            }
        }

        /// <summary>
        /// Converts a DateTime object into an SQL compatible string.
        /// </summary>
        /// <param name="dt">The DateTime object.</param>
        /// <returns>An SQL formatted string.</returns>
        public static string DateTimeToSQL(DateTime dt)
        {
            return dt.ToSQLDateTime();
        }
    }
}
