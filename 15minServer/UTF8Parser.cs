using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _15minServer
{
    class Utf8Parser : IParser
    {
        string pattern = "[^0-9]+";
        string replacement = "\t";

        public string Parse(byte[] data)
        {
            Regex rgx = new Regex(pattern);
            return rgx.Replace(Encoding.UTF8.GetString(data, 0, data.Length), replacement);
        }
    }
}
