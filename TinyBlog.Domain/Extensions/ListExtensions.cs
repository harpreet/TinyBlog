using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {

        public static bool HasData<T>(this IEnumerable<T> list)
        {
            if (list == null) return false;

            if (list.Count() == 0) return false;

            return true;
        }
    }
}
