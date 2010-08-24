using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SharpBag.BagDatabase
{
    /// <summary>
    /// Extension methods for the BagDB class.
    /// </summary>
    public static class BagDBExt
    {
        /// <summary>
        /// Converts a DateTime object into a SQL compatible string.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <returns>An SQL formatted string.</returns>
        public static string ToSQLDateTime(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Generates a SQL insert query for the current DataTable instance.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <param name="schema">The schema to insert into.</param>
        /// <returns>An SQL string.</returns>
        public static string ToSQL(this DataTable dt, string schema)
        {
            if (dt.Rows.Count == 0) return null;
            string into = schema != null ? schema + "." + dt.TableName : dt.TableName;

            StringBuilder columns = new StringBuilder();
            List<string> nonPrimaryCols = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                if (columns.ToString() != "") columns.Append(",");
                if (!dt.PrimaryKey.Contains(col)) nonPrimaryCols.Add(col.ColumnName);

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
                        values.Append("'" + ((DateTime)o).ToSQLDateTime() + "'");
                    }
                    else if (new Type[] { typeof(int), typeof(double), typeof(float), typeof(decimal), typeof(Single) }.Contains(col.DataType))
                    {
                        values.Append(o.ToString());
                    }
                    else
                    {
                        values.Append("'" + o.ToString().SQLEscape() + "'");
                    }
                }
                values.Append(")");
            }

            StringBuilder updCols = new StringBuilder();
            foreach (string nCol in nonPrimaryCols)
            {
                if (updCols.ToString() != "") updCols.Append(",");
                updCols.Append(nCol + "=VALUES(" + nCol + ")");
            }

            return "INSERT INTO " + into + " (" + columns.ToString() + ") VALUES " + values.ToString() + (dt.PrimaryKey.Length == 0 && dt.Columns.Count - dt.PrimaryKey.Length > 0 ? "" : " ON DUPLICATE KEY UPDATE " + updCols.ToString()) + ";";
        }

        /// <summary>
        /// Inserts the current DataTable instance into the specified MySQL database.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <param name="db">The MySQL database to insert into.</param>
        /// <returns>How many rows were affected.</returns>
        public static int InsertInto(this DataTable dt, BagDatabase.BagDB db)
        {
            return db.Execute(dt.ToSQL(db.Schema));
        }

        /// <summary>
        /// Escapes the string for SQL insertion.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>The escaped string.</returns>
        public static string SQLEscape(this string s)
        {
            return MySqlHelper.EscapeString(s);
            //return s.Replace(@"\", @"\\").Replace(@"\'", "'").Replace(@"'", @"\'");
        }
    }
}
