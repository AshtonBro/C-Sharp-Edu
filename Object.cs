﻿using System;

namespace AshtonBro.Code
{
    /*
        <---------------------------- Базовый тип Object в C#  --------------------------------------->

        TODO: Преобразовать методы ToString, Equals, GetHashCode
        TODO: Поработать с остальными методами на практике.
        TODO: Создайте клон объекта
    */

    class Object
    {
        class myClass
        {

        }

        class Point
        {
            public int X { get; set; } // value type
            public myClass MyClass { get; set; } // Чтобы увидеть разницу при клонировании между value type и reference type
            public Point Y { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is Point point) // если obj является пойнтом, положит тип пойнт в X
                {
                    return point.X == X;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return X;
            }

            public override string ToString()
            {
                return X.ToString() + " ToString Method";
            }

            public Point Clone()
            {
                return MemberwiseClone() as Point;
            }

            public Point DeepClone()
            {
                var result = (Point)MemberwiseClone();
                return result.Y = Y.DeepClone();
            }

            public new Type GetType()
            {
                return typeof(UInt16);
            }
        }

        public static void RunDemo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            object obj = new object();

            int i = 5;
            int j = 4;

            Console.WriteLine(i.Equals(j)); // будут сравнены значения 5 и 4.

            var oi = (object)i;

            object oj = j;

            var p1 = new Point() { X = 5 };
            var p2 = new Point() { X = 5 };

            Console.WriteLine(p1.Equals(p2));
            Console.WriteLine(null == null);

            Console.WriteLine(i.GetHashCode());
            Console.WriteLine(oj.GetHashCode());
            Console.WriteLine(new myClass().GetHashCode());
            Console.WriteLine(p1.GetHashCode());

            Console.WriteLine(i.ToString()); // value type
            Console.WriteLine(p1.ToString()); // reference type

            Console.WriteLine(i.GetType());
            Console.WriteLine(oi.GetType());
            Console.WriteLine(p1.GetType());

            Console.WriteLine(typeof(Point) == p1.GetType());

            Console.WriteLine(Object.Equals(5, 5));
            Console.WriteLine(Object.ReferenceEquals(5, 5));
            Console.WriteLine(Object.ReferenceEquals(p2, p2));

            var pp = new Point() { X = 7, Y = new Point() };
            var pp2 = pp;
            pp2.X = 77;
            pp2.Y = new Point() { X = 99 };
            Console.WriteLine(pp);
            Console.WriteLine(pp.Y);

            var pp3 = pp.Clone();
            pp3.X = 99;
            pp3.Y.X = 222;
            Console.WriteLine(pp);
            Console.WriteLine(pp.Y);
            Console.WriteLine(pp3.Y);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
