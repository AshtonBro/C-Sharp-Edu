using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AshtonBro.Code
{
    /*
<------------------- LINQ и работа с коллекциями в C#  ------------------>

    TODO: Перебрать коллекцию из DATABASE с помощью LINQ

    Операции выбора
    ElementAt получаем элемент из коллекции по индексу
    var elementAt = products.ElementAt(5);

    Single() Из всей коллекции выберем первый и ЕДИНСТВЕННЫЙ элемент у которого значение будет ровно 10, если встречается два или более равные 10 получаем exception
    var single = products.Single(prod => prod.Energy == 10);

    FirstOrDefault лучше чем просто First - FirstOrDefault забирает первый элемент из коллекции, в том случае, если массив пустой, то присваивает дефолтное значение, для int это 0;
    var first = array.FirstOrDefault();

    LastOrDefault лучше чем просто Last - LastOrDefault забирает последний элемент из коллекции, в том случае, если массив пустой, то присваивает дефолтное значение, для int это 0;
    var last = array.LastOrDefault();

    Take операция забирает заданное количество элементов с начало массива
    var sum3 = array.Take(2).Sum();

    Skip пропускает заданное количество элементов в массиве
    var sum3 = array.Skip(2).Sum();

        Агрегатные функции
    var sum = array.Sum();
    var min = array.Min();
    var max = array.Max();
    var min2 = products.Min(prod => prod.Energy);
    var max2 = products.Max(prod => prod.Energy);
    var aggregate = array.Aggregate((x, y) => x * y);

    Операции со множествами
    Операция Except возвращает те элементы которые есть в первом множестве, но нет во втором множестве(из первого множества вычли второе множество)
    операция вернёт 3,4
    var except = array.Except(array2);
    foreach (var item in except)
    {
        Console.WriteLine(item);
    }

    Операция Intersect возвращает те значения которым два массива пересекаются new int[] {1, 2, 3, 4 }; и new int[] { 3, 4, 5, 6 }; 
    операция вернёт 3,4
    var intersect = array.Intersect(array2);
    foreach (var item in intersect)
    {
        Console.WriteLine(item);
    }


    Union обЪеденяет двух множеств
    var array = new int[] {1, 2, 3, 4 };
    var array2 = new int[] { 3, 4, 5, 6 };
    foreach (var item in array)
    {
        Console.WriteLine(item);
    }

    var union = array.Union(array2);

    foreach (var item in union)
    {
        Console.WriteLine(item);
    }
    Console.ReadLine();

    Distinct удаляет из коллекции все дублирующиеся(повторяющиеся) элементы, т.е. 1,2,3,1,2,3 на выходе получим 1,2,3


    Операция Contains возвращает true если условие например (5) есть в указанной коллекции, если нет такого значения в коллекции то false
    products.Contains(products[3])

    Операции All и Any 
    All - возвращает true если условия соответствуют все элементы в коллекции, else false
    Any - возвращает true если условия соответствуют хотя бы для одного элемента в коллекции, else false
    products.All(item => item.Energy == 10) false
    products.Any(item => item.Energy == 10) true

    также можем использовать Reverse(); он просто разворачивает список верх ногами или задом на перед
    products.Reverse();

    Упорядочивание с помощью GroupBy  
    Dictionary<int, List<Product>>
    var groupByCollection = products.GroupBy(products => products.Energy);
    foreach (var group in groupByCollection)
    {
        Console.WriteLine($"Key: {group.Key}");
        foreach (var item in group)
        {
            Console.WriteLine($"\t{item}");
        }
    }
    Key: 195
            Продукт 0: (195)
    Key: 316
            Продукт 1: (316)
    Key: 421
            Продукт 2: (421)
    Key: 277
            Продукт 3: (277)
    Key: 85
            Продукт 4: (85)
    Key: 209
            Продукт 5: (209)
    Key: 153
            Продукт 6: (153)
    Key: 185
            Продукт 7: (185)
    Key: 149
            Продукт 8: (149)
    Key: 432
            Продукт 9: (432)


    Дополнительное упорядочивание ThenBy, работает вместо с OrderBy и инициализируется после OrderBy.
    var orderByCollection = products.OrderBy(products => products.Energy).ThenBy(products => products.Name);

    Сортировка коллекции через OrderBy
    var orderByCollection = products.OrderBy(products => products.Energy);
    foreach (var item in orderByCollection)
    {
        Console.WriteLine(item);
    }

    Преобразование из одного типа в другой через LINQ
    select в LINQ преобразовывает в тип, не как в SQL
    получаем коллекцию целых чисел

    При коллекции IEnumerable есть возможность поиска значений c помощью LINQ
    var result2 = collection.Where(item => item < 5).Where(item => item % 2 == 0).OrderByDescending(item => item);

     */
    public class Linq
    {
        static Random rnd = new Random();
        static List<Product> products = new List<Product>();

        public static void RunDemo()
        {

            #region Поиск условия в коллекции с помощью lambda
            var collection = new List<int>();

            for (int i = 0; i < 30; i++)
            {
                collection.Add(i);
            }

            var result2 = from item in collection
                         where item < 5
                         select item;

            foreach (var item in result2)
            {
                Console.WriteLine(item);
            }
            #endregion

            for (int i = 0; i < 10; i++)
            {
                var product = new Product()
                {
                    Name = $"Продукт {i}",
                    Energy = rnd.Next(10, 120)
                };

                products.Add(product);
            }

            var result = from item in products
                         where item.Energy < 200
                         select item;

            var result3 = products.Where(item => item.Energy < 200 || item.Energy > 400);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            foreach (var item in result3)
            {
                Console.WriteLine(item);
            }

            var selectCollection = products.Select(product => product.Energy);
            foreach (var item in selectCollection)
            {
                Console.WriteLine(item);
            }

            var orderByCollection = products.OrderBy(products => products.Energy).ThenBy(products => products.Name);
            foreach (var item in orderByCollection)
            {
                Console.WriteLine(item);
            }

            var groupByCollection = products.GroupBy(products => products.Energy);
            foreach (var group in groupByCollection)
            {
                Console.WriteLine($"Key: {group.Key}");
                foreach (var item in group)
                {
                    Console.WriteLine($"\t{item}");
                }
            }

            products.Reverse();
            foreach (var item in products)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(products.All(item => item.Energy == 10));
            Console.WriteLine(products.Any(item => item.Energy == 10));

            Console.WriteLine(products.Contains(products[3]));
            var prod = new Product();
            Console.WriteLine(products.Contains(prod));

            var array = new int[] { 1, 2, 3, 4 };
            var array2 = new int[] { 3, 4, 5, 6 };
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            var union = array.Union(array2);
            foreach (var item in union)
            {
                Console.WriteLine(item);
            }

            var intersect = array.Intersect(array2);
            foreach (var item in intersect)
            {
                Console.WriteLine(item);
            }

            var exepct = array.Except(array2);
            foreach (var item in exepct)
            {
                Console.WriteLine(item);
            }

            var sum = array.Sum();
            var min = array.Min();
            var max = array.Max();
            var min2 = products.Min(prod => prod.Energy);
            var max2 = products.Max(prod => prod.Energy);
            var aggregate = array.Aggregate((x, y) => x * y);
            Console.WriteLine(aggregate);

            var sum3 = array.Skip(2).Take(2).Sum();

            var first = array.FirstOrDefault();
            var last = array.LastOrDefault();
            var single = products.Single(prod => prod.Energy == 10);
            var elementAt = products.ElementAt(5);

            Console.ReadLine();
        }
    }

    class Product
    {
        public string Name { get; set; }
        public int Energy { get; set; }
        public override string ToString()
        {
            return $"{Name}: ({Energy})";
        }
    }

}
