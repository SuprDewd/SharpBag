﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace SharpBag.Database
{
    /// <summary>
    /// Extension methods for the BagDB class.
    /// </summary>
    public static class DatabaseExtensions
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
        /// Inserts the current DataTable instance into the specified database.
        /// </summary>
        /// <param name="dt">The current instance.</param>
        /// <param name="db">The database to insert into.</param>
        /// <returns>How many rows were affected.</returns>
        public static int InsertInto<T>(this DataTable dt, GenericDB<T> db) where T : DbConnection, new()
        {
            return db.Execute(db.DataTableToSQL(dt, db.Schema));
        }
    }
}