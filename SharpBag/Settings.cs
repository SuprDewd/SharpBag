﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SharpBag
{
    /// <summary>
    /// A static class containing settings and other related data.
    /// </summary>
    public static class Settings
    {
        private static DirectoryInfo _ExecutableDirectory = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

        /// <summary>
        /// Gets the directory where the executable is located.
        /// </summary>
        public static DirectoryInfo ExecutableDirectory { get { return _ExecutableDirectory; } }

        private static string _NL = Environment.NewLine;

        /// <summary>
        /// Gets the newline string defined for this environment.
        /// </summary>
        public static string NL
        {
            get { return _NL; }
            set { _NL = value; }
        }

        private static Dictionary<int, string> _BitTorrentTrackerErrorCodes = new Dictionary<int, string>
                                                                                  {
            {100, "Invalid request type: client request was not a HTTP GET."},
            {101, "Missing info_hash."},
            {102, "Missing peer_id."},
            {103, "Missing port."},
            {150, "Invalid infohash: infohash is not 20 bytes long."},
            {151, "Invalid peer_id: peer_id is not 20 bytes long."},
            {152, "Invalid numwant. Client requested more peers than allowed by tracker."},
            {200, "info_hash not found in the database."},
            {500, "Client sent an eventless request before the specified time."},
            {900, "Generic error."}
        };

        /// <summary>
        /// BitTorrent tacker error codes and what they mean.
        /// </summary>
        public static Dictionary<int, string> BitTorrentTrackerErrorCodes
        {
            get { return _BitTorrentTrackerErrorCodes; }
        }

        private static Dictionary<int, string> _HttpStatusCodes = new Dictionary<int, string>
                                                                      {
            {100, "Continue"},
            {101, "Switching Protocols"},
            {102, "Processing"},

            {200, "OK"},
            {201, "Created"},
            {202, "Accepted"},
            {203, "Non-Authoritative Information"},
            {204, "No Content"},
            {205, "Reset Content"},
            {206, "Partial Content"},
            {207, "Multi-Status"},

            {300, "Multiple Choices"},
            {301, "Moved Permanently"},
            {302, "Found"},
            {303, "See Other"},
            {304, "Not Modified"},
            {305, "Use Proxy"},
            {306, "Switch Proxy"},
            {307, "Temporary Redirect"},

            {400, "Bad Request"},
            {401, "Unauthorized"},
            {402, "Payment Required"},
            {403, "Forbidden"},
            {404, "Not Found"},
            {405, "Method Not Allowed"},
            {406, "Not Acceptable"},
            {407, "Proxy Authentication Required"},
            {408, "Request Timeout"},
            {409, "Conflict"},
            {410, "Gone"},
            {411, "Length Required"},
            {412, "Precondition Failed"},
            {413, "Request Entity Too Large"},
            {414, "Request-URI Too Long"},
            {415, "Unsupported Media Type"},
            {416, "Requested Range Not Satisfiable"},
            {417, "Expectation Failed"},
            {418, "I'm a teapot"}, // FTW!
            {422, "Unprocessable Entity"},
            {423, "Locked"},
            {424, "Failed Dependency"},
            {425, "Unordered Collection"},
            {426, "Upgrade Required"},
            {449, "Retry With"},
            {450, "Blocked by Windows Parental Controls"},

            {500, "Internal Server Error"},
            {501, "Not Implemented"},
            {502, "Bad Gateway"},
            {503, "Service Unavailable"},
            {504, "Gateway Timeout"},
            {505, "HTTP Version Not Supported"},
            {506, "Variant Also Negotiates"},
            {507, "Insufficient Storage"},
            {509, "Bandwidth Limit Exceeded"},
            {510, "Not Extended"}
        };

        /// <summary>
        /// Http status codes and what they mean.
        /// </summary>
        public static Dictionary<int, string> HttpStatusCodes
        {
            get { return _HttpStatusCodes; }
        }
    }
}