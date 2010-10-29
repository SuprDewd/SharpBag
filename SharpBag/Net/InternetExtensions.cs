using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SharpBag.Net
{
    /// <summary>
    /// Extensions methods for the BagNet class.
    /// </summary>
    public static class InternetExtensions
    {
        /// <summary>
        /// Downloads the source of the Uri and returns it as a string.
        /// </summary>
        /// <param name="page">The source of the Uri.</param>
        /// <returns></returns>
        public static string Download(this Uri page)
        {
            WebRequest request = HttpWebRequest.Create(page);
            StringBuilder results = new StringBuilder();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string line = null;

                    while ((line = sr.ReadLine()) != null)
                    {
                        results.AppendLine(line);
                    }

                    sr.Close();
                }
                response.Close();
            }
            return results.ToString();
        }

        /// <summary>
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s)
        {
            return s.HtmlSafe(false, false);
        }

        /// <summary>
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s, bool all)
        {
            return s.HtmlSafe(all, false);
        }

        /// <summary>
        /// Makes the current instance HTML safe.
        /// </summary>
        /// <param name="s">The current instance.</param>
        /// <param name="all">Whether to make all characters entities or just those needed.</param>
        /// <param name="replace">Whether or not to encode spaces and line breaks.</param>
        /// <returns>An HTML safe string.</returns>
        public static string HtmlSafe(this string s, bool all, bool replace)
        {
            int[] entities = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 28, 29, 30, 31, 34, 39, 38, 60, 62, 123, 124, 125, 126, 127, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 215, 247, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 8704, 8706, 8707, 8709, 8711, 8712, 8713, 8715, 8719, 8721, 8722, 8727, 8730, 8733, 8734, 8736, 8743, 8744, 8745, 8746, 8747, 8756, 8764, 8773, 8776, 8800, 8801, 8804, 8805, 8834, 8835, 8836, 8838, 8839, 8853, 8855, 8869, 8901, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 924, 925, 926, 927, 928, 929, 931, 932, 933, 934, 935, 936, 937, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 962, 963, 964, 965, 966, 967, 968, 969, 977, 978, 982, 338, 339, 352, 353, 376, 402, 710, 732, 8194, 8195, 8201, 8204, 8205, 8206, 8207, 8211, 8212, 8216, 8217, 8218, 8220, 8221, 8222, 8224, 8225, 8226, 8230, 8240, 8242, 8243, 8249, 8250, 8254, 8364, 8482, 8592, 8593, 8594, 8595, 8596, 8629, 8968, 8969, 8970, 8971, 9674, 9824, 9827, 9829, 9830 };
            string ut = "";
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (all || entities.Contains(c))
                {
                    ut += "&#" + ((int)c).ToString() + ";";
                }
                else
                {
                    ut += c.ToString();
                }
            }
            return (replace ? ut.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace(" ", "&nbsp;") : ut);
        }
    }
}