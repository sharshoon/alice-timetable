using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alice_timetable
{
    public static class Utils
    {
        public static bool ContainsStartWith(this IEnumerable<string> list, string start)
        {
            return list.Any(elem => elem.ToLower().Trim().StartsWith(start));
        }

    }
}
