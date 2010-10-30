﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.Common;

namespace SharpBag.Database
{
    /// <summary>
    /// A class for working with MySQL databases.
    /// </summary>
    public class MySqlDB : GenericDB<MySqlConnection>
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        /// <param name="schema">The default schema.</param>
        /// <param name="username">The username used to connect.</param>
        /// <param name="password">The password used to connect.</param>
        public MySqlDB(string server, string schema, string username, string password) : base(server, schema, username, password) { }

        /// <summary>
        /// Escapes a string for use in an SQL query.
        /// </summary>
        /// <param name="s">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        public override string SQLEscape(string s)
        {
            return MySqlHelper.EscapeString(s);
        }

        /// <summary>
        /// Kills the current thread and then closes the connection.
        /// </summary>
        public override void Close()
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
        /// Creates a query command.
        /// </summary>
        /// <param name="q">The query.</param>
        /// <param name="c">The connection.</param>
        /// <returns>The query command.</returns>
        protected override DbCommand CreateCommand(string q, MySqlConnection c)
        {
            return new MySqlCommand(q, c);
        }

        /// <summary>
        /// Creates connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        protected override string CreateConnectionString()
        {
            return "SERVER=" + this.Server + ";DATABASE=" + this.Schema + ";UID=" + this.Username + ";PWD=" + this.Password + ";";
        }
    }
}