using System.Data.Common;
using System.Data.SqlClient;

using System.Diagnostics.Contracts;

using System.Text;

namespace SharpBag.Database
{
    /// <summary>
    /// A class for working with MySQL databases.
    /// </summary>
    public class MicrosoftSqlDatabase : GenericDatabase<SqlConnection>
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        /// <param name="schema">The default schema.</param>
        /// <param name="username">The username used to connect.</param>
        /// <param name="password">The password used to connect.</param>
        public MicrosoftSqlDatabase(string server, string schema, string username, string password)
            : base(server, schema, username, password)
        {
            Contract.Requires(server != null);
            Contract.Requires(schema != null);
            Contract.Requires(username != null);
            Contract.Requires(password != null);
        }

        /// <summary>
        /// Escapes a string for use in an SQL query.
        /// </summary>
        /// <param name="s">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        public override string SQLEscape(string s)
        {
            // Contract.Requires(s != null);
            return s.Replace(@"\", @"\\").Replace(@"\'", "'").Replace(@"'", @"\'");
        }

        /// <summary>
        /// Creates a query command.
        /// </summary>
        /// <param name="q">The query.</param>
        /// <param name="c">The connection.</param>
        /// <returns>The query command.</returns>
        protected override DbCommand CreateCommand(string q, SqlConnection c)
        {
            // Contract.Requires(!String.IsNullOrEmpty(q));
            // Contract.Requires(c != null);

            return new SqlCommand(q, c);
        }

        /// <summary>
        /// Creates connection string.
        /// </summary>
        /// <returns>The connection string.</returns>
        protected override string CreateConnectionString()
        {
            return new StringBuilder("SERVER=").Append(this.Server).Append(";DATABASE=").Append(this.Schema).Append(";UID=").Append(this.Username).Append(";PWD=").Append(this.Password).Append(";").ToString();
        }
    }
}