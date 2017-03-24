using System;
using System.Collections.Generic;
using System.Text;

namespace FunSharp.Core.Extensions
{
    public static class IntExtensions
    {
        public static int AddList(this IEnumerable<int> list)
        {
            int total = 0;
            foreach (int i in list)
            {
                total += i;
            }
            return total;
        }
    }
}
