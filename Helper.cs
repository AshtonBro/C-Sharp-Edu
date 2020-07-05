using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// можно собрать все методы расширения в одном файле
namespace AshtonBro.Code
{
    public static class Helper
    {
        public static bool IsEvenValue(this int i)
        {
            return i % 2 == 0;
        }

        public static bool IsDevidedValue(this int i, int j)
        {
            return i % j == 0;
        }

        public static string ConvertToString(this IEnumerable collection)
        {
            var result = "";
            foreach (var item in collection)
            {
                result += item.ToString() + ", ";
            }
            return result;
        }
    }
}
