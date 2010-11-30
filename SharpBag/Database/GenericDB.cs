using System;
using System.Data;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;

namespace SharpBag.Database
{
    /// <summary>
    /// A class for working with databases.
    /// </summary>
    /// <typeparam name="ConnT">The type of the connection.</typeparam>
    public abstract class GenericDB<ConnT> where ConnT : DbConnection, new()
    {
        /// <summary>
        /// The server to connect to.
        /// </summary>
        public string Server { get; protected set; }
        /// <summary>
        /// The default schema.
        /// </summary>
        public string Schema { get; protected set; }
        /// <summary>
        /// The username used to connect.
        /// </summary>
        public string Username { get; protected set; }
        /// <summary>
        /// The password used to connect.
        /// </summary>
        public string Password { get; protected set; }
        /// <summary>
        /// The time before the connection times out.
        /// </summary>
        public int CommandTimeout { get; protected set; }
        /// <summary>
        /// The state of the connection.
        /// </summary>
        public ConnectionState State { get { return this.Connection.State; } }

        /// <summary>
        /// The connection.
        /// </summary>
        public ConnT Connection { get; protected set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        /// <param name="schema">The default schema.</param>
        /// <param name="username">The username used to connect.</param>
        /// <param name="password">The password used to connect.</param>
        public GenericDB(string server, string schema, string username, string password)
        {
            this.CommandTimeout = 120;
            this.Server = server;
            this.Schema = schema;
            this.Username = username;
            this.Password = password;
            this.Connect();
        }

        /// <summary>
        /// Connects, or reconnects, to the database.
        /// </summary>
        public void Connect()
        {
            if (this.Connection == null) this.Connection = new ConnT();
            if (this.Connection.State != ConnectionState.Open)
            {
                this.Connection.ConnectionString = this.CreateConnectionString();
                this.Connection.Open();
            }
        }

        /// <summary>
        /// Send a query to the database.
        /// </summary>
        /// <param name="q">The query string.</param>
        /// <returns>A DataTable object with the results from the query.</returns>
        public virtual DataTable Query(string q)
        {
            Monitor.Enter(this.Connection);
            this.Connect();
            DataTable dt = null;
            try
            {
                using (DbCommand cmd = this.CreateCommand(q, this.Connection))
                {
                    cmd.CommandTimeout = this.CommandTimeout;
                    using (DbDataReader reader = cmd.ExecuteReader())
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
        /// Send a query to the database and returns a scalar value.
        /// </summary>
        /// <param name="q">The query string.</param>
        /// <returns>The scalar value.</returns>
        public virtual object QuerySingle(string q)
        {
            this.Connect();
            Monitor.Enter(this.Connection);
            try
            {
                using (DbCommand cmd = this.CreateCommand(q, this.Connection))
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
        /// Send a query to the database and returns a scalar value, as the type of T.
        /// </summary>
        /// <typeparam name="T">The type of the first column.</typeparam>
        /// <param name="q">The query string.</param>
        /// <returns>The scalar value, as the type of T.</returns>
        public virtual T QuerySingle<T>(string q)
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
        /// Executes a query on the database and returns how many rows were affected.
        /// </summary>
        /// <param name="q">The query to execute.</param>
        /// <returns>How many rows were affected.</returns>
        public virtual int Execute(string q)
        {
            this.Connect();
            Monitor.Enter(this.Connection);
            try
            {
                using (DbCommand cmd = this.CreateCommand(q, this.Connection))
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
        /// Closes the connection.
        /// </summary>
        public virtual void Close()
        {
            if (this.Connection.State != ConnectionState.Closed)
            {
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

        /// <summary>
        /// Generates a SQL insert query for the specified DataTable.
        /// </summary>
        /// <param name="dt">The DataTable.</param>
        /// <param name="schema">The schema to insert into.</param>
        /// <returns>An SQL string.</returns>
        public virtual string DataTableToSQL(DataTable dt, string schema)
        {
            if (dt.Rows.Count == 0) return null;
            string into = schema != null ? schema + "." + dt.TableName : dt.TableName;

            StringBuilder columns = new StringBuilder();
            List<string> nonPrimaryCols = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                if (columns.ToString() != "") columns.Append(",");
                if (!dt.PrimaryKey.ContainsArray(col)) nonPrimaryCols.Add(col.ColumnName);

                columns.Append(col.ColumnName);
            }

            StringBuilder values = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                values.Append(values.ToString() == "" ? "(" : ",(");
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.Columns.IndexOf(col) != 0) values.Append(",");
                    object o = row[col.ColumnName];
                    if (col.DataType == typeof(DBNull))
                    {
                        values.Append("NULL");
                    }
                    else if (col.DataType == typeof(DateTime))
                    {
                        values.Append("'").Append(((DateTime)o).ToSQLDateTime()).Append("'");
                    }
                    else if (new Type[] { typeof(int), typeof(double), typeof(float), typeof(decimal), typeof(Single) }.ContainsArray(col.DataType))
                    {
                        values.Append(o.ToString());
                    }
                    else
                    {
                        values.Append("'");
                        values.Append(this.SQLEscape(o.ToString()));
                        values.Append("'");
                    }
                }
                values.Append(")");
            }

            StringBuilder updCols = new StringBuilder();

            if (dt.PrimaryKey.Length > 0)
            {
                foreach (string nCol in nonPrimaryCols)
                {
                    if (updCols.ToString() != "") updCols.Append(",");
                    updCols.Append(nCol);
                    updCols.Append("=VALUES(");
                    updCols.Append(nCol);
                    updCols.Append(")");
                }
            }

            return new StringBuilder("INSERT INTO ").Append(into).Append(" (").Append(columns.ToString()).Append(") VALUES ").Append(values.ToString()).Append(dt.PrimaryKey.Length == 0 && dt.Columns.Count - dt.PrimaryKey.Length > 0 ? "" : " ON DUPLICATE KEY UPDATE ").Append(updCols.ToString()).Append(";").ToString();
        }

        /// <summary>
        /// Creates connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        protected abstract string CreateConnectionString();
        /// <summary>
        /// Creates a query command.
        /// </summary>
        /// <param name="q">The query.</param>
        /// <param name="c">The connection.</param>
        /// <returns>The query command.</returns>
        protected abstract DbCommand CreateCommand(string q, ConnT c);
        /// <summary>
        /// Escapes a string for use in an SQL query.
        /// </summary>
        /// <param name="s">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        public abstract string SQLEscape(string s);
    }
}