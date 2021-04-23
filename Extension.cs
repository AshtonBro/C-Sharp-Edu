using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//< --------- Методы расширения(Extension Method) в C# ------------->

//TODO: Создать метод расширения для стандартного типа данных
//TODO: Создать метод расширения для интерфейса
//TODO: Создать метод расширения для seаled-класса из внешний библиотеки

// можно собрать все методы расширения в одном файле
namespace AshtonBro.Code
{
    public class Extension
    {
        public static void RunDemo()
        {
            while (true)
            {
                Console.Write("Введите число: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if (result.IsEvenValue())
                    {
                        Console.WriteLine($"{result} - Четное.");
                    }
                    else
                    {
                        Console.WriteLine($"{result} - Нечетное.");
                    }

                    int h = 182;
                    h.IsDevidedValue(7);

                    var list = new List<Road>();

                }
                else
                {
                    if (input.Contains("q"))
                    {
                        Console.WriteLine("Программа завершена");
                        break;
                    }

                    Console.WriteLine("Введите корректное число");
                }
            }

            Console.ReadLine();

            var roads = new List<Road>();

            for (int i = 0; i < 10; i++)
            {
                var road = new Road();
                road.CreateRandomRoad(1000, 10000);
                roads.Add(road);
            }

            var roadsName = roads.ConvertToString();
            Console.WriteLine(roadsName);
            Console.ReadLine();
        }
    }

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
                result += item.ToString() + ", \r\n";
            }
            return result;
        }

        public static Road CreateRandomRoad(this Road road, int min, int max)
        {
            var rnd = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));
            road.Number = "M" + rnd.Next(1, 100);
            road.Lenght = rnd.Next(min, max);
            return road;
        }
    }

    public sealed class Road
    {
        public string Number { get; set; }
        public int Lenght { get; set; }

        public override string ToString()
        {
            return $"Дорога: {Number}, Общей протяженностью: {Lenght}";
        }
    }
}
