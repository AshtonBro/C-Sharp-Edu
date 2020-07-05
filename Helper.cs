using System;
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
    }
}
