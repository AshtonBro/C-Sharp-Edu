using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AshtonBro.Code
{
    class Program
    {
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;

			var groups = new List<Group>();
			var students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
				var group = new Group(i, "Group: " + i);
				group.SetPrivate(i);
				groups.Add(group);
			}

			for(int i = 0; i < 300; i++)
            {
				var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100)
				{
					Group = groups[i % 9]
				};

				students.Add(student);

			}
			var binFormater = new BinaryFormatter();

			using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
				binFormater.Serialize(file, groups);
            }

			using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
			{
				var newGroups = binFormater.Deserialize(file) as List<Group>;

				if (newGroups != null)
                {
                    foreach (var group in groups)
                    {
						Console.WriteLine(group);
                    }
                }
			}

			Console.ReadLine();
		}
	}
}



/*
 
<---------------------------- Сериализация (serialization) объектов и работа с XML и JSON в C# --------------------------------------->






<---------------------------- Атрибуты (Attribute) и Рефлексия (Reflection) .NET в C# --------------------------------------->

TODO: Исследовать рефликсию
TODO: Реализовать получение свойства, классов и методов
TODO: Создать свой собственный атрибут
TODO: Использовать свой собственный атрибут в классе

Чтобы инициализировать Атрибут необходимо наследоваться от System.Attribute
Атрибут на практике применяется идеально подходит и используется при Серилизации (перевести его из формата объекта в текстовый формат JSOX, XML...)
Мы отмечает те классы которые ходим серилизовать и с помощью атрибутов отмечаем те свойства который для нас выжны во время серилизации.

При ASP.NET мы указываем какой тип, предмет, тип запроса GET,SET - c помощью атрибута помечаем, это позволяет компилятору явно понимать к какому именно из методов с одинаковыми именами обращатся

class GeoAttribute : System.Attribute
{
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)] // Используем атрибут AttributeTargets что присвоить к кокой структуре относить атрибут через точно можно
получить большой cписок структур
class GeoAttribute : System.Attribute
{
    public int X { get; set; }
    public int Y { get; set; }

    public GeoAttribute() { }
    public GeoAttribute(int x, int y)
    {
        // проверка входных данных
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return $"[ОсьX: {X}; ОсьY: {Y};]";
    }
}

public class Photo
{
    [Geo(14, 35)]
    public string Name { get; set; }
    public string Path { get; set; }
    public Photo(string name)
    {
        // Проверка входных параметров
        Name = name;
    }
}

class Program
{
	static void Main(string[] args)
	{
		Console.ForegroundColor = ConsoleColor.Green;

		var photo = new Photo("hello.png")
		{
			Path = @"C:\Program Files\hello.png"
		};

		var type = typeof(Photo);
        var attributes = type.GetCustomAttributes(false); // получили наши атрибуты в виде массива объекта
        foreach (var attr in attributes)
        {
            Console.WriteLine(attr);
        }

		var properties = type.GetProperties(); // возвращает коллекцию всех свойств класс
        foreach (var prop in properties)
        {
			var attrs2 = prop.GetCustomAttributes(false);

			if (attrs2.Any(a => a.GetType() == typeof(GeoAttribute))) // мы вывели только те свойства класса которые отмечены этим атрибутом 
			{
				Console.WriteLine(prop.PropertyType + " " + prop.Name);
			} 

				
			var attrs = prop.GetCustomAttributes(false);

            foreach (var a in attrs)
            {
                Console.WriteLine(a);
            }
        }

		Console.ReadLine();

	}
}

Чаще всего атрибуту задают без параметров, для того что была возможность задавать свойства с помощью их имени и делаем объявление атрибута достаточно хорошо читаемый
Если конструктор без параметров, то и атрибут мы можем создать пустой

var type = type-of(Photo); // базовый класс type, который является контейнеров хранения информации о классе


<---------------------------- Анонимные типы (Anonymous Type) и кортежи (ValueTuple и Tuple) в C# --------------------------------------->

TODO: Создать анонимный тип, Tuple и ValueTuple значения.
TODO: ValueTuple использовать в качестве аргумента метода и в качестве возвращаемого значения

Кортежи позволяют создавать наборы данных либо одного либо нескольких типов как в отличии от массивов, что массивы могут содержать только одного типо, кортежи могут содержать различные типы
и их также можно передавать в методы

Tuple<int, string> tuple = new Tuple<int, string>(5, "Hello");
Console.WriteLine(tuple.Item1);
Console.WriteLine(tuple.Item2); 
tuple.Item1 = 14; - также свойства tuple являются защищенные

Анонимные типо это можно сказать контейнеры для хранения данных, данные с анонимных типов можно только прочитать (GET) записать нельзя

class Car
{
    public string Name { get; set; }
}

class Program
{
	static void Main(string[] args)
	{
		Console.ForegroundColor = ConsoleColor.Green;

		var i = 5;

		var parking = new
		{
			Name = "BMW",
			Number = "R435RT72",
			Power = 234
		};

        Console.WriteLine(parking);
        Console.WriteLine($"Name: {parking.Name} Number: {parking.Number} Power: {parking.Power}");

		var car = new Car()
		{
			Name = "Toyota"
		};

		var parking2 = new
		{
			car.Name,
			Power = 234
		};

        Console.WriteLine(parking2);

		Console.ReadLine();
	}
}

var parking = new
{
	Name = "BMW",
	Number = "R435RT72",
	Power = 234
};


<---------------------------- Индексаторы (Indexer) и Итераторы (yield). Интерфейс IEnumerable в C# --------------------------------------->
 class Program
    {
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;

			var cars = new List<Car>()
			{
				new Car() { Name = "Ford", Number = "A001AA01" },
				new Car() { Name = "Toyota", Number = "B021AD74" },
				new Car() { Name = "Shkoda", Number = "C231CA22" }
			};

			var parking = new Parkink();

            foreach (var car in cars)
            {
				parking.Add(car);
			}

			foreach (var car in parking)
			{
				Console.WriteLine($"Парковка: {car}");
			}

			foreach (var car in parking.GetCarNames())
            {
                Console.WriteLine($"Name: {car}");
            }

			Console.WriteLine(parking["B021AD74"]?.Name);
			Console.WriteLine(parking["B021AD73"]?.Name);


            Console.Write("Введите номер автомобиля: ");
			var num = Console.ReadLine();
			parking[1] = new Car() { Name = "BMW", Number = num };
            Console.WriteLine(parking[1]);

			Console.ReadLine();
		}

	}


Чтобы наш класс смог работать с циклом foreach нам нужно чтобы этот класс реализовывал interface IEnumerable
Enumerator это Interface который возвращает нам коллекцию
Iterator это переборщиц нашей коллекции

Для того чтобы перебрать все элементы коллекции можно использовать yield

class Car
{
    public string Name { get; set; }
    public string Number { get; set; }

    public override string ToString()
    {
        return $"Авто: {Name}, Номер: {Number}";
    }
}

Сделали метод для доступа по индексу. parking["B021AD74"]?.Name передали в качестве параметра в [] скобка номер машины, в классе Parking оно пришло в индексатор,
через LINQ ищем номер в на нашей парковке в массиве и выдаём пользователю имя авто если оно есть или NULL в другом случае

class Parkink : IEnumerable
    {
        private List<Car> _cars = new List<Car>();
        private const int MAX_CARS = 100;

        public Car this[string number] // индексация, обращаемся к элементу по его индексу
        {
            get
            {
                var car = _cars.FirstOrDefault(c => c.Number == number);
                return car;
            }
            //set
            //{

            //}
        }

        public Car this[int position]
        {
            get
            {
                if(position < _cars.Count)
                {
                    return _cars[position];
                }

                return null;
            }

            set
            {
                if (position < _cars.Count)
                {
                    _cars[position] = value;
                }

            }
        }

        public int Count => _cars.Count; // Доступ на чтение(быстрое свойство), позволяет объявить публичное свойство которое предоставляет доступ к закрытому свойству с правами только на чтение
        public string Name { get; set; }
        public int Add(Car car)
        {
            if(car == null)
            {
                throw new ArgumentNullException(nameof(car), "Car is NULL");
            }

            if(_cars.Count < MAX_CARS)
            {
                _cars.Add(car);
                return _cars.Count - 1;
            }

            return -1;
        }

        public void GoOut(string number)
        {
            if(string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Number is NULL or empty");
            }

            var car = _cars.FirstOrDefault(c => c.Name == number);
            if(car != null)
            {
                _cars.Remove(car);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var car in _cars)
            {
                yield return car;
            }
        }

        public IEnumerable GetCarNames()
        {
            foreach (var car in _cars)
            {
                yield return car.Name;
            }
        }

    }

    
    public class ParkingEnumerator : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

public Car this[string number] // индексация, обращаемся к элементу по его индексу
{
    get
    {
        var car = _cars.FirstOrDefault(c => c.Number == number);
        return car;
    }
    //set
    //{

    //}
}


public тип this[тип индекс] // индексация, обращаемся к элементу по его индексу
{
    get
    {

    }
    set
    {

    }
}


<---------------------------- Методы расширения (Extension Method) в C# --------------------------------------->

TODO: Создать метод расширения для стандартного типа данных
TODO: Создать метод расширения для интерфейса
TODO: Создать метод расширения для seаled-класса из внешний библиотеки


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    class Program
    {
		
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;

			while(true)
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

static void Main(string[] args)
{
	Console.ForegroundColor = ConsoleColor.Green;

	while(true)
    {
		Console.Write("Введите число: ");
		var input = Console.ReadLine();

		if (int.TryParse(input, out int result))
		{
			var isEven = IsEvenValue(result);

			if (isEven)
			{
				Console.WriteLine($"{result} - Четное.");
			}
			else
			{
				Console.WriteLine($"{result} - Нечетное.");
			}

		}
		if(input.Contains("q"))
        {
			break;
        }
	}

	Console.ReadLine();
	
}

static bool IsEvenValue(int i)
{
	return i % 2 == 0;
}


<---------------------------- LINQ и работа с коллекциями в C#  --------------------------------------->

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

var selectCollection = products.Select(product => product.Energy);

foreach (var item in selectCollection)
{
    Console.WriteLine(item);
}
Console.ReadLine();




Выборка из класса

class Product
    {
        public string Name { get; set; }
        public int Energy { get; set; }
        public override string ToString()
        {
			return $"{Name}: ({Energy})";
        }
    }
    class Program
    {
		static Random rnd = new Random();
		static List<Product> collection = new List<Product>();

		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 10; i++)
            {
				var product = new Product()
				{
					Name = "Продукт " + i,
					Energy = rnd.Next(10, 500)
				};

				collection.Add(product);
            }
			
			var result = from item in collection
						 where item.Energy < 200 
						 select item;

			var result2 = collection.Where(item => item.Energy < 200 || item.Energy > 400);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
			Console.WriteLine("----------------");
			foreach (var item in result2)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

    }
class Program
    {
		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			var collection = new List<int>();

            for (int i = 0; i < 30; i++)
            {
				collection.Add(i);
            }

			var result = from item in collection
						 where item < 5 
						 select item;

			var result2 = collection.Where(item => item < 5);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            foreach (var item in result2)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }

При коллекции IEnumerable есть возможность поиска значений c помощью LINQ
var result2 = collection.Where(item => item < 5).Where(item => item % 2 == 0).OrderByDescending(item => item);

Поиск условия в коллекции с помощью lambda
static void Main(string[] args)
{
	Console.ForegroundColor = ConsoleColor.Green;
	var collection = new List<int>();

    for (int i = 0; i < 30; i++)
    {
		collection.Add(i);
    }

	var result = from item in collection
					where item < 5 
					select item;

    foreach (var item in result)
    {
        Console.WriteLine(item);
    }
	Console.ReadLine();
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
    class Program
    {
		static Random rnd = new Random();
		static List<Product> products = new List<Product>();

		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;

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

			var result2 = products.Where(item => item.Energy < 200 || item.Energy > 400);
			foreach (var item in result)
			{
				Console.WriteLine(item);
			}

			foreach (var item in result2)
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

<---------------------------- SQL базы данных и Entity Framework в C#  --------------------------------------->

При изменении и добавлении новой таблицы в класс, необходимо разрешить миграцию и обновить базу данных
Если мы работаем с базой данной в проекте единожды в Консоле диспетчера пакетов необходимо ввести команду enable-migrations
Далее уже если мы постоянно добавляем или изменяем таблицы также в этом же диспетчере вводим команду add-migration AddBandCountry что является коммитом после команды add-migration
далее вводим update-database.

context.Bands.RemoveRange(context.Bands); очистить таблицу

namespace AshtonBro.Code
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public int BandId { get; set; }

        public virtual Band Band { get; set; }
    }
}

namespace AshtonBro.Code
{
    public class Band
    {
        public int BandId { get; set; }
        public string Name { get; set; }
        public int?  Year { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}


namespace AshtonBro.Code
{
    public class MyDbContext : DbContext
    {
        protected MyDbContext() : base("DbConnectionString")
        {
        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}

 class Program
    {
		static void Main(string[] args)
        {
			using(var context = new MyDbContext())
            {
                //Country = "Armenia"
                //Country = "USA"
                //Country = "Germany"

                var bands = new List<Band>
                {
                    new Band() { Name = "AC/DC", Year = 1976 },
                    new Band() { Name = "Omph", Year = 2002 },
                    new Band() { Name = "CORN", Year = 1995 }
                };

                context.Bands.AddRange(bands);
                context.SaveChanges();

                var songs = new List<Song>
                {
                    new Song() { Name = "Toxicity", BandId = 16 },
                    new Song() { Name = "Smells like Teen Spirit", BandId = 17 },
                    new Song() { Name = "In bloom", BandId = 17 },
                    new Song() { Name = "Mutter", BandId = 18 }
                };

                context.Songs.AddRange(songs);
                context.SaveChanges();

                foreach (var song in songs)
                {
                    Console.WriteLine($"Songs: {song.Name}, Name: {song.Band?.Name}, Year: {song.Band.Year}");
                }

                Console.ReadLine();

            }
        }

    }

app.config

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
    <section name="entityFramework"
       type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=4.3.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
  </configSections>
  
  <connectionStrings>
    <add name="DbConnectionString"
         connectionString="data source=ASHTON\ASHTON;initial catalog=MusicAlboms;integrated security=True;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>

  <entityFramework>
    
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="mssqlocaldb" />
      </parameters>
    </defaultConnectionFactory>
    
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
    
  </entityFramework>
</configuration>


<---------------------------- Сокеты (socket) и клиент-серверное взаимодействие по протоколам TCP и UDP в C# --------------------------------------->

	TODO: Разделить приложение в своей предметной области на клиентскую и серверную части
	TODO: Реализовать отправку сообщений одним из двух протоколов


	!!! КЛИЕНТ UDP !!!
	 class ClientUDP
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8082;
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);
            while(true)
            {
                Console.Write("Введите сообщение: ");
                var message = Console.ReadLine();
                var serverUdpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081);
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverUdpEndPoint);
                var buffer = new byte[256];
                var sizeData = 0; 
                var data = new StringBuilder();
                EndPoint senderUdpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081); // экземпляр адреса в который будем записывать данные (сохранить данные подключения, адрес клиента)
                do
                {
                    sizeData = udpSocket.ReceiveFrom(buffer, ref senderUdpEndPoint); // через реферальный аргумент передаём наш sender
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                Console.WriteLine(data);
            }
        }
    }
	!!! СЕРВЕР UDP !!!
	 class ServerUDP
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сервер запущен...");
            const string ip = "127.0.0.1";
            const int port = 8081;
            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);
            while (true)
            {
                var buffer = new byte[256]; // хранилище данных
                var sizeData = 0; // переменная в которую будем записывать реальное кол-во байт
                var data = new StringBuilder();
                EndPoint senderUdpEndPoint = new IPEndPoint(IPAddress.Any, 0); // экземпляр адреса в который будем записывать данные (сохранить данные подключения, адрес клиента)
                do
                {
                    sizeData = udpSocket.ReceiveFrom(buffer, ref senderUdpEndPoint); // через реферальный аргумент передаём наш sender
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                udpSocket.SendTo(Encoding.UTF8.GetBytes("От Сервера: Сообщение получено"), senderUdpEndPoint);
                Console.WriteLine($"Сообщение от клиента: {data}");
            }
            //udpSocket.Shutdown(SocketShutdown.Both);
            //udpSocket.Close();
        }
    }


	!!! КЛИЕНТ !!!

	class ClientTcp
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            Console.Write("Введите сообщение: ");
            var message = Console.ReadLine(); // ввели сообщение

            var data = Encoding.UTF8.GetBytes(message); // получили и закодировали данные

            tcpSocket.Connect(tcpEndPoint); // открыть сокет, сделать подключение для этого сокета
            tcpSocket.Send(data); // Отправляем наш массив байт

            var buffer = new byte[256]; // Получаем ответ
            var sizeData = 0; 
            var answer = new StringBuilder(); // Ответ сервера

            do 
            {
                sizeData = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, sizeData));
            }
            while (tcpSocket.Available > 0); // получаем сообщение, раскодировали

            Console.WriteLine(answer.ToString());

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Console.ReadLine();
        }
    }


	!!! СЕРВЕР !!!

	class ServerTCP
    {
        static void Main(string[] args)
        {
            // задать адрес приложения Ip адрес и порт
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			tcpSocket.Bind(tcpEndPoint); // связываем endPoint с Socket, мы говорим нашему socket, что необходимо слушать
			tcpSocket.Listen(5);

			while(true) // клиент пришел, создали листенера, данные обработали, отправили ответ и уничтожили. Далее обрабатываем следующего
            {
				// обработчик на приём сообщения
				var listener = tcpSocket.Accept(); // создаётся новый сокет для подключения клиента
				var buffer = new byte[256]; // хранилище данных
				var sizeData = 0; // переменная в которую будем записывать реальное кол-во байт
				var data = new StringBuilder();

				do // проверяем условия что мы получили запрос
				{
					sizeData = listener.Receive(buffer);
					data.Append(Encoding.UTF8.GetString(buffer, 0, sizeData)); // кодируем и отправляем данные
				}
				while (listener.Available > 0);

				Console.WriteLine(data);

				listener.Send(Encoding.UTF8.GetBytes("Успех")); // принимаем и раскодируем данные

				listener.Shutdown(SocketShutdown.Both); // Отключаем подключение, двухсторонние отключение - закрываем у клиента и у сервера
				listener.Close(); // закрываем подключение
            }
        }
    }

  
<---------------------------- Асинхронность (async, await) и многопоточность (thread) в C# ---------------------------------------> 

	TOD: 1 - В своей предметной области создать метод со сложными вычислениями
	TOD: 1 - Сделать для этого метода обертку в виде async-метода
	TOD: 1 - Переписать свой код в асинхронном варианте

	TOD: 2 - Создать вручную поток (thread) 
	TOD: 2 - Сделать для него повышенный приоритет
	TOD: 2 - Запустить выполнение и попробовать завершить приложение

	TOD: 3 - Использовать lock

  class Program
    {
		public static object locker = new object();
		static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var result = AsyncSaveFileTxt("myStream.txt");

            Console.WriteLine(result);
            Console.ReadLine();

        }

		static Task<bool> AsyncSaveFileTxt(string path)
        {
			var result = Task.Run(() => SaveFileTxt(path));
			return result;
        }

        static bool SaveFileTxt(string path)
        {
			lock(locker)
            {
                var rnd = new Random();
                var text = "";

                for (int i = 0; i < 10000; i++)
                {
                    text += rnd.Next(i);
                }

                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine(text);
                }

                return true;
            }
		}

    }


	// Dead log
	class Program
    {
		public static object locker = new object();
		public static int i1 = 0;
		public static int i2 = 0;

		static void M1()
        {
            for (int i = 0; i <= i1; i++)
            {

            }
        }

        static void M2()
        {
            for (int i = 0; i >= i1; i--)
            {

            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

			var result = SaveFileAsync("stream.txt");
			var input = Console.ReadLine();
			Console.WriteLine(result.Result);
			Console.ReadLine();
        }

		static async Task<bool> SaveFileAsync(string path)
        {
			var result = await Task.Run(() => SaveFile(path));
			return result;
        }

		static bool SaveFile(string path)
        {
            lock (locker)
			{
                var rnd = new Random();
                var text = "";
                for (int i = 0; i < 50000; i++)
                {
                    text += rnd.Next();
                }

                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine();
                }

                return true;
            }
		}

		static async Task DoWorkAsync()
        {
			Console.WriteLine("Begin async");
			await Task.Run(() => DoWork(15)); // лямбда, анонимная конструкция
            Console.WriteLine("End async");
        }

		static void DoWork(int max)
        {
			int j = 0;
            for (int i = 0; i < max; i++)
            {
               Console.WriteLine("DoWork");
            }
        }

        static void DoWork2(object max)
        {

            int j = 0;
            for (int i = 0; i < (int)max; i++)
            {
                j++;

                if (j % 10000 == 0)
                {
                    Console.WriteLine("DoWork2");
                }
            }
        }

    }


	// Сделали функцию записывающию в файл txt текст затем сделали её асинхронное
	// т.е. теперь можно работать в консоле пока наша функция выполняется в другом потоке

	class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

			var result = SaveFileAsync("stream.txt");
			var input = Console.ReadLine();
			Console.WriteLine(result.Result);
			Console.ReadLine();
        }

		static async Task<bool> SaveFileAsync(string path)
        {
			var result = await Task.Run(() => SaveFile(path));
			return result;
        }

		static bool SaveFile(string path)
        {
			var rnd = new Random();
			var text = "";
            for (int i = 0; i < 50000; i++)
            {
				text += rnd.Next();
            }

			using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
				sw.WriteLine();
            }

			return true;
        }

		static async Task DoWorkAsync()
        {
			Console.WriteLine("Begin async");
			await Task.Run(() => DoWork(15)); // лямбда, анонимная конструкция
            Console.WriteLine("End async");
        }

		static void DoWork(int max)
        {
			int j = 0;
            for (int i = 0; i < max; i++)
            {
               Console.WriteLine("DoWork");
            }
        }

        static void DoWork2(object max)
        {

            int j = 0;
            for (int i = 0; i < (int)max; i++)
            {
                j++;

                if (j % 10000 == 0)
                {
                    Console.WriteLine("DoWork2");
                }
            }
        }

    }

	// можем передавать параметр
	static async Task DoWorkAsync()
        {
			Console.WriteLine("Begin async");
			await Task.Run(() => DoWork(15)); // лямбда, анонимная конструкция
            Console.WriteLine("End async");
        }

		static void DoWork(int max)
        {
			int j = 0;
            for (int i = 0; i < max; i++)
            {
               Console.WriteLine("DoWork");
            }
        }

	static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
			Console.WriteLine("Before DoWorkAsync");
			DoWorkAsync();
			Console.WriteLine("After DoWorkAsync");

			for (int i = 0; i < 10; i++)
            {
              Console.WriteLine("Main");
            }

			Console.WriteLine("End Main");
			Console.ReadLine();
        }

		static async Task DoWorkAsync()
        {
			Console.WriteLine("Begin async");
			await Task.Run(() => DoWork()); // лямбда, анонимная конструкция
            Console.WriteLine("End async");
        }

		static void DoWork()
        {
			int j = 0;
            for (int i = 0; i < 10; i++)
            {
               Console.WriteLine("DoWork");
            }
        }

            Thread thread = new Thread(new ThreadStart(DoWork));
            thread.Start();

            Thread thread2 = new Thread(new ParameterizedThreadStart(DoWork2));
            thread2.Start(int.MaxValue);

            int j = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                j++;

                if (j % 10000 == 0)
                {
                    Console.WriteLine("Main");
                }
            }


		static void DoWork()
        {
			int j = 0;
            for (int i = 0; i < 100; i++)
            {
                j++;

				if(j % 10000 == 0)
                {
                    Console.WriteLine("DoWork");
                }
            }
        }

        static void DoWork2(object max)
        {

            int j = 0;
            for (int i = 0; i < (int)max; i++)
            {
                j++;

                if (j % 10000 == 0)
                {
                    Console.WriteLine("DoWork2");
                }
            }
        }



 <---------------------------- Делегаты (delegate) и события (event) в C# ---------------------------------------> 

	TODO: Создать приложение, которое запрашивает данные пользователя
	TODO: Записывает введённые данные в файл
	TODO: По команде читает данные из файла


	// Открыть
	// Прочитать/Записать
	// Закрыть

	  static void Main(string[] args)
        {
			Console.OutputEncoding = Encoding.Unicode;

			using(var sw = new StreamWriter("stream.txt", true, Encoding.Unicode))
            {
				sw.Write("Hello ");
				sw.WriteLine("Hello User");
				sw.WriteLine("Привет Кириллица в потоках");
            }

			using(var sr = new StreamReader("stream.txt", Encoding.Unicode))
            {
				var str = sr.ReadToEnd(); // ReadToEnd прочитать файл от начало до конца
                Console.WriteLine(str);
            }
			Console.ReadLine();
        }

	using(var sw = new StreamWriter("stream.txt", true)) // Добавляем true вторым параметрам и тем самым разрешаем дописывать в файл, false перезаписывает - не дополняет
    {
		var str = "Hello";
		sw.Write(str);
		sw.WriteLine(str + "User");
    }

	using(var sw = new StreamWriter("stream.txt")) // так мы создаём файл сохраняем в него string и при запуске перезаписываем файл
    {
		var str = "Hello";
		sw.Write(str);
		sw.WriteLine(str + "User");
    }


 <---------------------------- Делегаты (delegate) и события (event) в C# ---------------------------------------> 
	public delegate void MyEvDelegate();
	// События (События обычно создаются через делегаты)
    public event MyEvDelegate Event;
	public event Action EventAction;



	public delegate тип_возвращаемого_значения имя_делегата(тип_аргумента аргумент)
	public delegate void MyFirstDelegate();
	Action<int, int, string> action1 = Method5; // от 1 до 16 перегрузок

	class Program
    {
		public delegate int ValueDelegate(int i);

		// группа делегатов Action которые не возвращают на значения, но могут принимать от 0 до 16 возможных значений
		public Action ActionDelegate; // одинаковые по сигнатуре ↓
		public delegate void MyFirstDelegate(); // одинаковые по сигнатуре ↑
		public delegate void Actions(int i); // сокращенно Action<int> action1 = Method2;
		public delegate bool Predicate<T>(T value); // Predicate<int> myPredict;
		public delegate int Func(string value); // Func<string, int> funct;
		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;

			MyFirstDelegate myDelegate = Method1;
			myDelegate += Method3;
            myDelegate();

            MyFirstDelegate myDelegate2 = new MyFirstDelegate(Method3);
			myDelegate2 += Method3; // add method in delegate 
			myDelegate2 -= Method3; // remove method in delegate 
			myDelegate2.Invoke();

			MyFirstDelegate myDelegate3 = myDelegate + myDelegate2; // Можем объединить делегаты в одном делегате;
			myDelegate3.Invoke();

			var valueDelegate = new ValueDelegate(MethodValue);
			valueDelegate += MethodValue; // --
			valueDelegate += MethodValue; //  | Метод вызывает все пять раз, но возвращается полученное значение только от последнего метода
			valueDelegate += MethodValue; //  | рандомное число 30 получили 5 раз
			valueDelegate += MethodValue; // --

			valueDelegate((new Random()).Next(10, 50));

			Action action = Method1; // сокращенный способ объявления делегата возвращающего ничего (тоже самое что: public delegate void MyFirstDelegate();) от 1 до 16 перегрузок
			action();

            Predicate<int> myPredict;

            Func<string, char, int> func;
			Func<int> func2;


			Func<int, int> func3 = MethodValue;
			// такая форма записи проверяет если фанк пустой то игнорируем если метод внутри есть то вызываем. это обработка исключения
			func3?.Invoke(7);  // расшифровка if (func3 != null) { func3(7); }


            Console.ReadLine();
        }

		public static int MethodValue(int i)
        {
            Console.WriteLine(i);
			return i;
        }

		public static void Method1()
        {
            Console.WriteLine("Запустился Метод 1");
        }

        public static int Method2()
        {
            Console.WriteLine("Запустился Метод 2");
			return 0;
		}
        public static void Method3()
        {
            Console.WriteLine("Запустился Метод 3");
        }
        public static int Method4()
        {
            Console.WriteLine("Запустился Метод 4");
			return 0;
        }

        public static int Method5(int i, int j)
        {
            Console.WriteLine("Запустился Метод 5");
			return i + j;
        }
    } 

 <---------------------------- Исключения (Exception) в C# ---------------------------------------> 

	class MyException : Exception
    {
        public MyException() : base ("Вызвалось собственное исключение")
        {
        }

		public MyException(string message) : base(message)
        {
        }

		public MyException(string message, Exception inner) : base (message, inner)
        {
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;

            try
            {
				throw new MyException("Вызываю моё исключение", new NullReferenceException());
				int q = 6;
				int j = q / 0;
				var x = new List<int>();
				x.ElementAt(5);
			}
			catch (MyException ex)
            {
                Console.WriteLine("В блоке catch вызываем MyExpetion");
                Console.WriteLine("Ошибка: {0}", ex.Message);
                if (ex.InnerException == null)
                    Console.WriteLine("Inner равен null: {0}", ex.InnerException);
				if(ex.InnerException != null)
                    Console.WriteLine("Inner неравен null: {0}", ex.InnerException);
            }
			catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Деление на ноль: {ex.Message}");
            }
			catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"За пределами индекса: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Работа звершена");
				Console.ReadLine();
			}
			
		}
	}

	// создаём своеё собственное исключение
    class MyOwnException : ArgumentException
    {
        public MyOwnException() : base("Моё исключение") {}
		public MyOwnException(string message) : base(message) {}
    }

    class Program
    {
        static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;

			int result = 0;

			while(true)
            {
				var input = Console.ReadLine();
				if(int.TryParse(input, out result))
                {
                    Console.WriteLine($"Корректно преобразованный в int: {result}");
					break;
                }
				else
                {
                    Console.WriteLine($"Некоректный ввод, введите целове число");
                }
            }

            Console.WriteLine(result);
			int i = 5;

            try
            {
				throw new MyOwnException();
            }
			catch (MyOwnException ex)
            {
                Console.WriteLine(ex.Message);
            }			
			catch (DivideByZeroException ex) when (i == 5)
            {
                Console.WriteLine("Исключение: " + ex.Message + " и i == 5");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
				throw ;
            }
			finally
            {
				Console.WriteLine("Работа завершена");
				Console.ReadLine();
            }
		}
	}

		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;


				int i = 5;
            try
            {
				throw new DivideByZeroException("i", "Пользователь делит на ноль");
            }
			catch (DivideByZeroException ex) when (i == 5)
            {
                Console.WriteLine("Исключение: " + ex.Message + " и i == 5");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
				throw ;
            }
			finally
            {
				Console.WriteLine("Работа завершена");
				Console.ReadLine();
            }
		}

		static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;

            try
            {
                int i = 5;
                var j = i / 1;
                Console.WriteLine(j);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
			finally
            {
				Console.WriteLine("Работа завершена");
				Console.ReadLine();
            }
		}

 
 <---------------------------- Интерфейсы C# (Interface) ---------------------------------------> 

// Домашнее задание
// Определить интерфейс содержащий методы и свойства 
// Определить интерфейс наследник от первого интерфейса (ещё методы)
// Реализовать второй интерфейс

// Явное и не явное опеределние интерфейса
	interface IPerson
    {
		int Move(int distance);
    }

    public class Cyborg : ICar, IPerson
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        int ICar.Move(int distance)
        {
            return distance / 100;
        }

		int IPerson.Move(int distance)
        {
			return distance / 5;
        }
    }
    interface ICar : IObject
    {
		/// <summary>
		/// Выполнить перемещение
		/// </summary>
		/// <param name="distance">Расстояние.</param>
		/// <returns>Время движения.</returns> 
		int Move(int distance);
    }
	
	// Последовательное наследование
	interface IObject
    {
		void Create();
    }

    class LadaSeven : ICar, IDisposable 
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Move(int distance)
        {
			return distance / 40;
        }
    }

	class LadaVesta : ICar
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public int Move(int distance)
        {
			return distance / 100;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			var cars = new List<ICar>();
			cars.Add(new LadaSeven());
			cars.Add(new LadaVesta());
			
			foreach (var car in cars)
            {
                Console.WriteLine(car.Move(450));
            }

			// Явное и не явное опеределние интерфейса
            var cyborg = new Cyborg();
            Console.WriteLine(((ICar)cyborg).Move(100));
            Console.WriteLine(((IPerson)cyborg).Move(100));

            Console.ReadLine();

		}
	}

// без модификатора доступа, название интерфейса начинается с I 
	interface ICar
    {
		void Move(int distance);
    }


 <---------------------------- Обобщения или шаблоны (Generic) в C# ---------------------------------------> 

-HOME WORK
В вашей предметной области определить базовый класс и несколько наследников
Создать класс который будет в качестве универсального типо базовый класс
Поэксперементировать с универсальными типами

 Значение по умолчании
 public Product(string name, T volume, T energy)
        {
            Name = name;
            Volume = volume;
			Energy = default(T);
        }

public class Product
    {
        public string Name { get; }
        public int Calorie { get; }
        public int Volume { get; set; }
        public int Energy { get; set; }
        
        public Product(string name, int calorie, int volume, int energy)
        {
            Name = name;
            Calorie = calorie;
            Volume = volume;
			Energy = energy;
        }
    }

	class Apple : Product
	{
		public Apple(string name, int calorie, int volume, int energy) : base(name, calorie, volume, energy)
		{

		}

	}

    class Banana : Product
    {
        public Banana(string name, int calorie, int volume, int energy) : base(name, calorie, volume, energy)
        {

        }
    }

	public class Eating<T, TT>
		where T: Product
		where TT: IEnumerable
	{
		public int Volume { get; private set; }
		public void Add(T product)
        {
			Volume += product.Volume * product.Energy;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
			var eating = new Eating<Banana, List<int>>();

			var list = new List<int>();
			
			var map = new Dictionary<int, string>();
			map.Add(5, "Пять");
			map.Add(5, "Пять");
		}
    }
	// Анонимный тип <T> (Tamplate)
	public class Product<T, TT>
    {
        public string Name { get; }
        public T Calorie { get; }
        public T Volume { get; set; }
        public TT Energy { get; set; }
        
            
        public Product(string name, T calorie, T volume, TT energy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (calorie < 0)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (volume <= 0)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Calorie = calorie;
            Volume = volume;
			Energy = energy;

        }


    }
    class Program
    {
        static void Main(string[] args)
        {
			var product = new Product<int, decimal>("Green Apple", 90, 100, 120);
			var product2 = new Product<decimal, int>("Banana", 90.2M, 100.10M, 23);
			var list = new List<int>();

																			-------------------------------------------------------------------<
			// дикшенери работает по принципу ключь, значение, ключь, значение - добавлять значения при помощи Add.
			// ключь это уникальное значение и добваить в дикшенери такой же ключь у вас не получится
			//map.Add(5, "Пять");
			// map.Add(5, "Пять"); нельзя, выкинит эксепшен в ран тайме 


			var map = new Dictionary<int, string>();
			map.Add(5, "Пять");
			map.Add(5, "Пять");
		}
    }

 <---------------------------- Перегрузка операторов (operator) в C#--------------------------------------->
// Домашнее задание

// Реализовать +, -, >, <, >=, <=, ==, != для класса из предметной области

 public abstract class Product
    {
        public string Name { get; }
		//	Калорийность на 100гр продукта
        public int Calorie { get; }
		// Обьём в граммах
        public int Volume { get; set; }
		public double Energy
        {
			get
            {
				return Volume * Calorie / 100.0;
            }
        }
		public Product(string name, int calorie, int volume)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
				throw new ArgumentNullException(nameof(name));
            }
            if (calorie < 0)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (volume <= 0)
            {
                throw new ArgumentNullException(nameof(name));
            }

			Name = name;
			Calorie = calorie;
			Volume = volume;

        }

        public override string ToString()
        {
			return $"{Name}. Calorie: {Calorie}. Volume: {Volume}";
        }
    }

    public class Apple : Product
    {
        public Apple(string name, int calorie, int volume) : base(name, calorie, volume)
        {

        }

		public static Apple Add(Apple apple1, Apple apple2)
        {
			int calories = (int)Math.Round(((apple1.Calorie + apple2.Calorie) / 2.0));
			var volume = apple1.Volume + apple2.Volume;
			var apple = new Apple("Apple", calories, volume);

			return apple;
        }
		// перегрузка операторов: +, -, *, /, %, ==, !=, < >, <= =>, ++, --, /=, *= (это возможные операторы которые мы можем переопределить по своему) 
		public static Apple operator +(Apple apple1, Apple apple2)
        {
            int calories = (int)Math.Round(((apple1.Calorie + apple2.Calorie) / 2.0));
            var volume = apple1.Volume + apple2.Volume;
            var apple = new Apple("Apple", calories, volume);

            return apple;
        }

        public static Apple operator +(Apple apple1, int volume)
        {
            var apple = new Apple(apple1.Name, apple1.Calorie, apple1.Calorie + volume);
            return apple;
        }

		public static bool operator== (Apple apple1, Apple apple2)
        {
			return apple1.Name == apple2.Name;
        }

        public static bool operator !=(Apple apple1, Apple apple2)
        {
            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Apple apple1 = new Apple("Red apple", 53, 100);
			Apple apple2 = new Apple("Green apple", 90, 120);

			var sumAplle = Apple.Add(apple1, apple2);
			var sumAplle2 = apple1 + apple2;

			var sumAplle3 = apple1 + 100;

            Console.WriteLine(apple1);
            Console.WriteLine(apple2);
            Console.WriteLine(sumAplle);
            Console.WriteLine(sumAplle2);
            Console.WriteLine(sumAplle3);
            Console.WriteLine(apple1 == apple2);
            Console.WriteLine(sumAplle == sumAplle2);

			Console.ReadLine();
		}
    }
<---------------------------- Методы C# (Method C#) классов --------------------------------------->
// модификатор доступа, тип возврашаемое значение, имя метода(тип аргумента имя аргуменат)

// отличатся сигнатурой, перегрузка (имя метода и принимаемые аргументы) при вызове методо появляются стрелочки верз и вних это перегрузки
		// 2 одинаковых имени дают выбор
		public string Run(int x, int y)
        {
			X += x;
			Y += y;

			return $"{Name} ({X} {Y})";
        }

		public string Run(double y)
        {
            return $"{Name} ({X} {Y})";
        }

	public static string PrintHello(string name, int age)
        {
			if(!String.IsNullOrEmpty(name))
            {
			 Console.WriteLine($"Hello, {name}. You have {age} ages/");
            }
			return "";
        }

		public static int Factorial(int value)
        {
			if(value <= 1)
            {
				return 1;
            }
			else
            {
				return value * Factorial(value - 1);
            }
        }

	Console.ForegroundColor = ConsoleColor.Green;
            var person1 = new Person("Wuik", "Jon");
            var person2 = new Person("Capone", "Jogan");

                     for (int i = 0; i < 10; i++)
                     {
            	var position1 = person1.Run();
                         Console.WriteLine(position1);

                         Console.WriteLine(person2.Run(person1));
                     }

                     Console.WriteLine();

            Console.WriteLine(Factorial(3));

            Console.ReadLine();

	public class Person
    {
		public string SecondName { get; set; }
		public string Name { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public Person(string secondName, string name)
        {
			if(!String.IsNullOrEmpty(secondName) && !String.IsNullOrEmpty(name))
            {
				SecondName = secondName;
				Name = name;
				X = 0;
				Y = 0;
            }
        }

		public string Run()
        {
			var rnd = new Random();
			X += rnd.Next(-2, 2);
			Y += rnd.Next(-2, 2);

			return $"{Name} ({X}, {Y})";
		}
    }

<----------------------------Классы (class), конструкторы (constructor) и свойства (property)------------------------------------------------->
<-----------------------------------------Объектно-ориентированное программирование (ООП) в C#. Инкапсуляция, наследование, полиморфизм #8---------------------------------------->
	// Домашнее задание
	// выбрать предметную область, товар, человек, животные и тд
	// Создать классы со свойствами из выбранной предметно области
	// Задать для них конструкторы

	public class Car
	{
		public string Model { get; set; }
		public string Color { get; set; }
		public string Engine { get; set; }
		public double EnginePower { get; set; }

		public Car(string model, string color, string engine, double enginepower)
        {
			if (!String.IsNullOrEmpty(model) && !String.IsNullOrEmpty(color) && !String.IsNullOrEmpty(engine))
			{
				if (model.Length < 20 && color.Length < 20 && engine.Length < 20)
				{
					Model = model;
					Color = color;
					Engine = engine;
				}
				else throw new Exception("String must be lower than 20");
			}
			else throw new Exception("String can't be null or Empty");

			if (enginepower < 250 && enginepower > 0)
			{
				EnginePower = enginepower;
			}
			else throw new Exception("This is Urban transport, is can't be too powerful");
        }
	}

	public class CheapTunnig : Car
    {
		public string Marka { get; set; }
        public CarAdd(string model, string color, string engine, double enginepower)
			: base(model, color, engine, enginepower)
		{
		}
	}

	Ловим ошибки перед записью в переменную
	public class Person
	{
		public string Name { get; private set; }
		public string SecondName { get; private set; }
		public int Age { get; set; }
		public Person(string name, string secondName, int age)
		{
			if (String.IsNullOrWhiteSpace(name) || name.Length < 2)
			{
				throw new Exception(String.Format("Incorrect Name"));
			}
			else
			{
				Name = name;
			}

			if (String.IsNullOrWhiteSpace(secondName) || secondName.Length < 2)
			{
				throw new Exception(String.Format("Incorrect Name"));
			}
			else
			{
				SecondName = secondName;
			}

			if (age < 0 || age > 120)
            {
				throw new Exception(String.Format("Age don't must be lower than 0"));
            }
			else
            {
				Age = age;
            }
        }
	}

	public class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Person person = new Person("Bob", "Brown", 450);

            Console.WriteLine(String.Join(" ", person.Name, person.SecondName, person.Age));
			Console.ReadLine();
		}
	}

	public string Name { get; set; } = "Tom";

	// Интерпаляция строк
	$"{SecondName} {Name.Substring(0, 1)}.";

	// Вычисляемые свойства, свойство которое зависит от других свойств
		public string FullName
        {
			get
            {
				return $"{SecondName} {Name}";
            }
        }

		public string ShortName
        {
			get
			{
				return $"{SecondName} {Name.Substring(0, 1)}.";
            }
        }

// ещё сокращаем
	public string SecondName { get; set; }

// сокращаем

   private string _name;
        public string Name
        { get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

 private string _name;
        public string Name
        { get
            {
                return _name;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentNullException("The name isn't can empty");
                }
            }
        }

class Person
		{
			public string FirstName;
			public string SecondName;

			private decimal Money;
		}

		class Doctor : Person
		{
			public string Profession;
		}

		static void Main(string[] args)
		{
			int i = 0;
			Int32 j = new Int32();

			Person person = new Person();
			person.FirstName = "Volodja";
			person.SecondName = "Gavrilov";

			Person person1 = new Person();
			person1.FirstName = "Ivan";
			person1.SecondName = "Ivanov";

			Doctor doctor = new Doctor();
			doctor.FirstName = "Ross";
			doctor.Profession = "Serjun";

			Console.WriteLine(doctor.FirstName);
			Console.WriteLine(doctor.Profession);
			// полиморфизм
			Person p = doctor;
			Console.WriteLine(p.FirstName);
			Doctor dd = (Doctor)p;

			Console.WriteLine(dd.FirstName);
			Console.WriteLine(dd.Profession);
			Console.ReadLine();
		}
	}

<------------------------------------------------Циклы C# (for, foreach, while)------------------------------------->

		int[,] array = new int[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
					array[i, j] = 8;
				}
            }

		foreach (var item in list)
            {
                Console.WriteLine(item + " Hello");
            }

		List<string> list = new List<string>();

            for (int i = 0; i < 50; i += 2)
            {
				list.Add(i.ToString());
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
			Console.ReadLine();

			while(list.Count < 5) // повторять до дех пор пока выпонятся какой-то условие
			{
				list.Add(Console.ReadLine());
			}

			var j = 0;
			while(j < list.Count)
            {
                Console.WriteLine(list[j]);
				j++;
            }

			Console.ReadLine();

   for (int i = 0; i < 100; i += 5)
            {
                Console.WriteLine(i);
            }
			for (; ; ) // бесконечный цикл
			{
			}

			for (int i = 10 ; ; i += 5)
			{
			}
			Console.ReadLine();

			while(условие) // повторять до дех пор пока выпонятся какой-то условие
			{
			}

			do // если аналогично условие не верное, то хотябы один раз тело выполнится.
			{
			}
			while (j < list.Count);

<----------------- Home Work --------------->

	public class Age
		{
			public int Id { get; set; }
			public int Experians { get; set; }
			public int Years { get; set; }
		}
		static void Main(string[] args)
		{
			List<Age> ages = new List<Age>();
			ages.Add(new Age() { Id = 1, Experians = 10, Years = 5});
			ages.Add(new Age() { Id = 6, Experians = 17, Years = 10 });
			ages.Add(new Age() { Id = 5, Experians = 14, Years = 37 });
			ages.Add(new Age() { Id = 9, Experians = 12, Years = 85 });
			ages.Add(new Age() { Id = 3, Experians = 7, Years = 11 });
			ages.Add(new Age() { Id = 7, Experians = 9, Years = 55 });
			ages.Add(new Age() { Id = 4, Experians = 28, Years = 5 });
			ages.Add(new Age() { Id = 8, Experians = 9, Years = 33 });

			var result = ages.Where(f => f.Experians > 10 | f.Years > 15).OrderBy(f => f.Id);

			foreach (var r in result)
			{
				Console.WriteLine("Id: " + r.Id + ", " + "Experians: " + r.Experians + ", " + "Years: " + r.Years);
			}

			Console.ReadLine();
		}

<------------------------------Коллекции C#: массивы (array) и списки (list). Перечисления (enum)--------------------->

 // Одномерный массив
int[] array = new int[10];
// Двумерный массив
int[,] array2d = new int[10, 10];
// Трёх мерный массив
int[,,] array3d = new int[10, 10, 10];

int[] array1d = { 1, 2, 3, 4, 5, 6, 7};

int[] arrayInit = new int[] { 0, 2, 3, 4, 5 };

array[0] = 5;
array[1] = 12;
array[2] = 17;

Console.WriteLine("{0}, {1}, {2}, {3}, {4}", array);
Console.WriteLine();
Console.ReadLine();

Если у нас массив динамический и постоянно меняетются, изменяются или добавляются значения, то лучше использовать список - list <>

чтобы объявить список, пишем list, a в кадратный скобках <> тип переменных которые будут находится в list

 // Список list <тип переменных внутри списка> имя списка = обьявляем как новый список с интами внутри
List<int> list = new List<int>();
list.Add(0);
list.Add(1);
list.Add(2);
list.Add(3);

List<int> ListTwo = new List<int>()
{
	1, 2, 3, 4, 5, 6, 7, 8
};

ListTwo.AddRange(array);

Console.WriteLine(array[0]);
Console.WriteLine(list[0]);
Console.WriteLine(Days.Fri);

int[] arrayFor = new int[23];

for (int i = 0; i <= arrayFor.Length - 1; i++)
{
	arrayFor[i] = i;
}
Console.WriteLine(string.Join(",", arrayFor));

List<int> listFor = new List<int>();
for (int j = 0; j <= arrayFor.Length - 1; j++)
{
	listFor.Add(j * 5);
}

Console.WriteLine(string.Join(",", listFor));

// преобразование list в array
int[] arrayList = listFor.ToArray();

string[] arrayStr = new string[] {"a", "b", "c", "f", "c", "a", "hg", "as", "32s4", };
List<string> listStr = new List<string>();

for (int q = 0; q <= arrayStr.Length - 1; q++)
{
	listStr.Add(arrayStr[q]);
}

// string.Join(",", array) сцепляет элементы созданной коллектции предварительно указав первым параметром сепаратор.
Console.WriteLine(string.Join(",", listStr));
Console.WriteLine(string.Join(",", arrayStr));
Console.ReadLine();

Console.WriteLine(array[0]);
Console.WriteLine(list[0]);
Console.WriteLine(Days.Fri);

Console.ReadLine();

FirstClass[] cs = new FirstClass[10];
List<FirstClass> css = new List<FirstClass>();

 enum Days
{
	Mon = 14,
	Tru = 23,
	Wen = 17,
	Tro = 96,
	Fri = 84,
	Sut = 35,
	Sun = 53
}

Советы по кментариям:
На начальном этапе писать коментарии практически ко всему, чтобы выучить и при повторном просмотре кода понять что мы тут написали,
конечно с практикой стараться писать комментарий все меньше, но при не стардартном поведении кода или функции всегда указывать комментарий

Console.WriteLine(); // Вывод текста на консоль.

* Много
* строчный
* коментарий
*

									 Приведение и преобразование типов C#
<------------------------------------- Type casting and conversion C#------------------------>

string s = "1"; You can't put string in int
int i = s; // error
int j = 5;
double d = j; // no error - int in double can be put, but on the contrary it can not be done
int k = d; // error

byte b = 42; // Possible value is from 0 to 255;
int bb = b; // Possible value from -2 billion to 2 billion

 int i = 5;
int j = 2038;
byte b = (byte)i; // error
byte bb = (byte)j; // error too
// with the help of forced type casting by opening the brackets before the one we can convert some
types to others, but it's better to avoid it

 // не явное приведение типов
byte b = 42;
int i = b;

// явное приведение типов (byte)ii
int ii = 5;
int j = 2038;
//byte b = (byte)ii; // error
byte bb = (byte)j; // error too

string s = "2048";
//int jo = (int)s; // error

// Явное преобразование
// но мы можем обратиться к специальному классу Convert для преобразование типов
int jojo = Convert.ToInt32(s);
int qo = int.Parse("42");
int qoqo = int.Parse(s);
int qoqoqo = Int32.Parse(s);
// int qoqoqoqo = int.TryParse();  Лучше использовать этот метод, но разберём его позже

// Не явное преобразование
// Чаще использую при работе со строками
int integer = 107;
string strieng = "str " + integer; // все в языке C# наследуется от типа object, и у него есть местод toString(),
// конвертация, приведение, пробразование в строку есть у кажного типа обьекта в C#, всегда можно преобразовать в формат.
// компилятор сам пониманиет что нужно преобразовать интеджер и склеить все строки, также это выглядило вот так
string striengTwo = "str " + integer.ToString();

// преобразование bool тип
bool boolian = true;
string str = boolian.ToString();
string secontStr = "false";
bool secondBoolian = Convert.ToBoolean(secontStr);

// tryParse
if (int.TryParse(Console.ReadLine(), out int result))
{
	Console.WriteLine(result);
} else
{
	Console.WriteLine("Input only integer");
}

Console.WriteLine(str);
Console.WriteLine(secondBoolian);
Console.WriteLine(jojo);
Console.WriteLine(b);
Console.WriteLine(bb);

int inter = 5;
string strOne = "1";
bool booll = true;
decimal dec = 7;
decimal res = dec / 2;
decimal dividend = Decimal.Multiply(dec, res);
double convertVar = Convert.ToDouble(booll);
string str = Convert.ToString(booll).ToUpper();
int mix = Int32.Parse(convertVar + strOne) * inter;

Console.WriteLine(mix);
Console.WriteLine(str);
Console.WriteLine(booll);
Console.WriteLine(dividend);
Console.ReadLine();

string UpperCamelCase; // PascalCase, first letter uppercase. (C#)
string lowerCameCase; // PascalCase, first letter lowercase. (C#)
string snake_case; // Snake style (JS)
string FAT_SNAKE_CASE; // Fat snake style, usually use for constant's
string kebab-case; // -#-#-#-#-- Kebab
string sHungarianCase; // Hungarian notation (C++)


namespace SimpleAlgorithmsApp
{
    public class LinkedList<T> : IEnumerable<T>  // односвязный список
    {
        Node<T> head; // головной/первый элемент
        Node<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке
 
        // добавление элемента
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);
 
            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;
 
            count++;
        }
        // удаление элемента
        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;
 
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        // убираем узел current, теперь previous ссылается не на current, а на current.Next
                        previous.Next = current.Next;
 
                        // Если current.Next не установлен, значит узел последний,
                        // изменяем переменную tail
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        // если удаляется первый элемент
                        // переустанавливаем значение head
                        head = head.Next;
 
                        // если после удаления список пуст, сбрасываем tail
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    return true;
                }
 
                previous = current;
                current = current.Next;
            }
            return false;
        }
 
        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        // очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        // содержит ли список элемент
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }
        // добвление в начало
        public void AppendFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            node.Next = head;
            head = node;
            if (count == 0)
                tail = head;
            count++;
        }
        // реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
 
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}

<==================================== MS DAY 1 ==========================================>

  Order or = new Order();
or.amount = 0;
myFunc(or);
Console.WriteLine(or.amount);

Customer sd = new Customer();
sd.id = 555;
myFunc2(sd);
Console.WriteLine(sd.id);

{
	Product pr = new Product();
	pr.Price = 3.13;
}

// Common language specification
Console.ForegroundColor = ConsoleColor.Green;
Console.BackgroundColor = ConsoleColor.Black;

var q = 333;
var j = "bla";

Int32 x = (int)3.14;
Int64 x2 = 650;
int i = 444;
Double dd2 = 3.13;
double dd = 3.14;

x2 = Int64.MaxValue;
DateTime z = DateTime.Now;

Int64 abc = int.Parse("123");

Console.WriteLine(x);
Console.WriteLine(x2);
Console.WriteLine(dd);
Console.WriteLine(z.ToUniversalTime());
Console.WriteLine(z.ToString("dd-MMM-yyyy"));

String str = "Hello!"; // EMMUTABLE
var str2 = "Hello!";
var str3 = "Hi";
str3 += "bla"; // BAD!!

str3 = String.Intern(str3);

if (Object.ReferenceEquals(str, str3))
{
	Console.WriteLine("Equal!!");
}

// Optimization work with  memory ise StringBuilder
StringBuilder sb = new StringBuilder("He", 100);
sb.Append("l");
sb.Append("l");
sb.Append("o");
sb.Append("!");
var str4 = sb.ToString();

if (Object.ReferenceEquals(str, str4))
{
	Console.WriteLine("Equal!!");
}
Console.WriteLine(str4);
// IMMUTABEL ARRAY  мы не можем поменять размер массива
// 1 int = 4byte //select.count
int[] myArray = new int[10] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // При случае если мы знаем колличество массива заранее
myArray[0] = 333;
myArray[1] = 555;

foreach (var elem in myArray)
{
	Console.WriteLine(elem);
}

int[, ,] array3d = new int[3, 5, 10]; // EMMUTABLE
array3d[1, 3, 7] = 777;

foreach (var elem in array3d)
{
	Console.WriteLine(string.Join(", ",elem));
}

int[,] array2d = new int[3, 5]; // EMMUTABLE
array2d[1, 3] = 777;

foreach (var elem in array2d)
{
	Console.WriteLine(elem);
}

// Обьявляем квадратный масив
int[][] arrayQ = new int[3][]; // EMMUTABLE
arrayQ[0] = new int[5];
arrayQ[1] = new int[7];
arrayQ[2] = new int[7];

foreach (var elem in arrayQ)
{
	Console.WriteLine(elem);
}

// namespace колизия имен, если делать библиотеку то будет конфликт функции которые называется по дефолту
// Неоднозначность убераем namespace
AshtonBro.CodeBlog._2.Program p = new AshtonBro.CodeBlog._2.Program();
Program p2 = new Program();
AshtonBro.CodeBlog._2.Program p3 = new Program();

// В операционной системе есть 2 типа операционной памяти Hit и Stack
// Virtual Memory Manager
// Thread (поток) -> ~ 05mb => 1.5mb quick - stack
// 8Tb -> slow
{
	Customer с = new Customer();
}

{
	Order o = new Order(); // будет существовать до скобок потом очищается. или выхода из функции
} // Clear stack

{
	Order o = new Order();
}

{
	int xx = 333; //automatic variable
}

int testVariable = new int();
testVariable = 333;
int yOut;
TestParam(ref testVariable, out yOut);
Console.WriteLine(testVariable);

Program pX = new Program();

try
{
	pX.XFunc();
}
catch (OutOfMemoryException ex)
{
	Console.WriteLine(ex.Message);
	EventLog.WriteEvent("Application", new EventInstance(333, 555));
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
	EventLog.WriteEvent("Application", new EventInstance(333, 555));
	Debug.WriteLine("This is debug error");
	Trace.WriteLine("This is debug error");
}

AshtonBro.CodeBlog._2.Program.myFunc(null);

Order o2 = new Order();
o2.amount = 0;
myFunc(o2);
Console.WriteLine(o2.amount);

Customer cc = new Customer();
cc.id = 555;
myFunc(cc);
Console.WriteLine(cc.id);

FUNCTIONS -------------------------------->
class Customer // Reference Type
{
	public int id;
	public double GetBalance()
	{
		AshtonBro.CodeBlog._2.Program.myFunc(null);
		return 3.14;
	}
}
struct Order
{
	public int amount;
}
class Product
{
	public double Price;
}

public bool XFunc()
{
	//Exception ex = new Exception("my error");
	//if (1 == 1)
	//throw ex;
	return false;
}

public bool XFunc2()
{
	//Exception ex = new Exception("my error");
	//if (1 == 1)
	//throw ex;
	return false;
}

static void myFunc(Order or) // Global func with name with name space
{
	or.amount = 100;
}

public static string myFunc(Customer cc, int z = 333)
{
	cc.id = -335;
	return "Hello";
}
static void TestParam(ref int x, out int y)
{
	y = 163;
	x++;
}

<==================================== MS DAY 2 ==========================================>
Необходимо понимать какой тип данных принимать в свой проект

enum myColor : int
	{
		Red = 255,
		Green = 65535,
		Blue = 481246
	}

	// Конструктор
	struct Order
	{
		int id;
		int CustID;
		int Amount;
		public Order(int _id, int _CustId, int _Amount)
		{
			id = _id;
			CustID = _CustId;
			Amount = _Amount;
		}
		public Order(string _id, int _CustId)
		{
			id = int.Parse(_id);
			CustID = _CustId;
			Amount = 0;
		}
		// функции доступа данных
		public void setID(int i)
		{
			if(i > 0)
			{
				id = i;
			} else {
				Exception ex = new Exception("ID must be above zero");
				throw ex;
			}
		}
		public int getID()
		{
			return id;
		}

	   // public int MyID { get; set; }
		public string this[string Name]
		{
			get { return "Hello" + Name; }
			set { }
		}
		public int ID { get { return id; } set {
				if (value < 0)
				{
					Exception ex = new Exception("ID must be above zero");
					throw ex;
				}
				id = value; }
		}
	}
	class Program
	{
		static void myFunction(myColor color)
		{
			Order bla = new Order();
		}
		static void Main(string[] args)
		{
			Order or2 = new Order();

			var s = or2["Bob"];

			or2.ID = 3333; // Get
			Console.WriteLine(or2.ID);
			Order or = new Order(163, 72, 73);

			Console.WriteLine(or);

			// иногда необходимо контролировать тип ввода данных, например я принимаю только 3 цвета и всё.
			myFunction(myColor.Blue);
			Console.WriteLine(myColor.Blue);
			// чтобы получить чисто нужно переконвертировать в int
			int x = (int)myColor.Blue;
			Console.WriteLine(x);
			Console.ReadLine();

			// Коллекции ----------------------------------------------
			ArrayList lst = new ArrayList(100); // прожерливая функция наъодиться в System.Collection - лучше избегать
			int i = 333;
			// boxing -> unboxind
			lst.Add(i); // для работы требуют согласовать вызов перед обработкой данных 4 byte -> 4 byte <- и того 8 byte на обратобку
			Stack sS = new Stack();
			sS.Push(i);  // всё что System.collections обходить
			// SortedList // боллее менее хороший вариант, но есть и лучше

			object obj = lst[0];
			int xIn = (int)obj;
		}
	}

 // DELEGATE -> string Invoke (string)
	delegate string MyServerEvent(int id);
	class Server
	{
		public MyServerEvent srvEvent;
		public void FireEvent()
		{
			if(srvEvent != null)
			{
				var result = srvEvent.Invoke(333);
			}
		}
	}
	class MyClient
	{
		public string MyFunction(int xInt)
		{
			Console.WriteLine("Event resived " + xInt.ToString());
			return "Hello from client!";
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Server srv = new Server();
			MyClient newCl = new MyClient();
			MyServerEvent mSerEv = new MyServerEvent(newCl.MyFunction);
			MyClient newCl2 = new MyClient();
			MyServerEvent mSerEv2 = new MyServerEvent(newCl.MyFunction);
			srv.srvEvent = mSerEv + mSerEv2;
			srv.srvEvent -= mSerEv2;
			srv.FireEvent();

			Console.ReadLine();
		}
	}

// Creating Class

	// Encapculation, все данные должны подконтрольно защищены.
   class Product
	{
		// 1 правило ООП - Все данные ООП должны быть защищены от сторонних вмешательство
		// если данные не доступны и с наружи не доступны, указываем их с нижнем подчёркиванием
		int Id;
		string _name;
		double _price;

		public double Price
		{
			get;
			set;
		}

		public double GetPrice()
		{
			if (1 == 1)
				return _price;
			else
				return 0.0;
		}
		public void SetPrice(double price)
		{
			if (price > 0)
				_price = price;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			Product p = new Product();
		   // p. = -3.14;
		}
	}

// Inheritance наследование
	//abstract class BankAccount // Чисто абстрактный класс - abstract
	//{
	//    // абстрактный класс создаём шаблон для дальнейшей модификации
	//    public abstract double GetBalance(); // функция пустышка, значем что она будет переписана чайелдом
	//}

	interface IBankAccount
	{
	   // Интерфейс шаблон и прототипы функции
	   // Класс у которого нет тела и функции пустышка называется PURE ABSTRACT CLASS - INTERFACE
		double GetBalance();
	}

	class ChildAccount : IBankAccount
	{
		int _id; // приватное поле, маленький регистр или нижнее подчёркивани
		protected double _amount;
		public double GetBalance()
		{
			return _amount + 3;
		}
		// при необходимости модифицирировать функуцию родителя пишем override к родителю добавляем виртальную функцию
	}
	class Program
	{
		static void CalculateBalance(IBankAccount b)
		{
			Console.WriteLine(b.GetBalance());
		}
	   static void Main(string[] args)
		{
			ChildAccount newChild = new ChildAccount();

			CalculateBalance(newChild); // Всё ок! мы наследовали методы и переменные с родителя

			Console.ReadLine();
		}
	}

// REFERENCE TYPES AND VALUE TYPES
 Reference types = class
 Value types = struct

// Inheritance наследование
	//abstract class BankAccount // Чисто абстрактный класс - abstract
	//{
	//    // абстрактный класс создаём шаблон для дальнейшей модификации
	//    public abstract double GetBalance(); // функция пустышка, значем что она будет переписана чайелдом
	//}

	interface IBankAccount
	{
	   // Интерфейс шаблон и прототипы функции
	   // Класс у которого нет тела и функции пустышка называется PURE ABSTRACT CLASS - INTERFACE
		double GetBalance();
	}

	class ChildAccount : IBankAccount
	{
		int _id; // приватное поле, маленький регистр или нижнее подчёркивани
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		protected double _amount;

		public double Amount
		{
			get { return _amount; }
			set { _amount = value; }
		}

		public double GetBalance()
		{
			return _amount + 3;
		}
		// при необходимости модифицирировать функуцию родителя пишем override к родителю добавляем виртальную функцию

		// Глобальные функция, можно вызвать на примую обращаяст к child
		public static string GetStatic()
		{
			return "Static Func";
		}
	}
	class Program
	{
		static void CalculateBalance(IBankAccount b)
		{
			Console.WriteLine(b.GetBalance());
		}
	   static void Main(string[] args)
		{
			var result = ChildAccount.GetStatic();
			Console.WriteLine(result);

			ChildAccount newChild = new ChildAccount() { Id = 333, Amount = 3.14 };

			CalculateBalance(newChild); // Всё ок! мы наследовали методы и переменные с родителя

			Console.ReadLine();
		}
	}

  // Introducing Generics (Template)

	class myGenericClass<T>
	{
		T _unknown;
		public T GetUnknown()
		{
			return _unknown;
		}
	}

	class Program
	{
		//static void MyFunction(object obj)
		//{
		//    Console.WriteLine(obj);
		//}
		// функция заточена забирать данные из стека, функция с неизвестным параметрам, при разных
		// типах даных сама подставляет необходимый тип и забирает и при это работает без box и unbox
		// настройка типа на ходу.
		static void MyFunction<UnknownDataType>(UnknownDataType obj)
		{
			Console.WriteLine(obj);
		}

		static void Main(string[] args)
	   {
			int x = 333;
			MyFunction(x);
			MyFunction("Я и это сьел");
			MyFunction(DateTime.Now);

			myGenericClass<int> cls1 = new myGenericClass<int>();
			Console.WriteLine(cls1.GetUnknown());

			myGenericClass<DateTime> cls2 = new myGenericClass<DateTime>();
			Console.WriteLine(cls2.GetUnknown());

			Console.ReadLine();
		}
	}

 // Introducing Generics (Template)
	class MyClass<T> where T : struct
	{
		T _unknown;
		public T GetUnknown()
		{
			return _unknown;
		}
	}
	class Program
	{
		static void Main(string[] args)
	   {
			//ArrayList lst = new ArrayList();
			//lst.Add(333);

			List<int> lst = new List<int>();
			lst.Add(163); // добавляем int
			var result = lst[0]; // выдаём int

			MyClass<int> cls1 = new MyClass<int>();
			Console.WriteLine(cls1.GetUnknown());

			MyClass<DateTime> cls2 = new MyClass<DateTime>();
			Console.WriteLine(cls2.GetUnknown());

			MyClass<double> cls3 = new MyClass<double>();

			Console.WriteLine(result);
			Console.ReadLine();
		}
	}

<==================================== MS DAY 3 ==========================================>

 // Virtual abstract

	class BankAccount
	{
		int _data = 555;

		public int Data { get => _data; set => _data = value; }
		public virtual int GetData() { return _data; } //vtbl virtual function table
	}

	class ChildAccount : BankAccount
	{
		public override int GetData()
		{
			Data = -333;
			return Data;
		}
	}
	class Program
	{
		static void Main(string[] args)

		{   // Member heen
			BankAccount b = new ChildAccount();
			var result = b.GetData();
			Console.WriteLine(result);
			Console.ReadLine();
		}
	}

 class BankAccount
	{
		int _data = 555;

		public int Data { get => _data; set => _data = value; }
		public virtual int GetData() { return _data; } //vtbl virtual function table
	}

	// Также можно закрыть выводиться из CildAccount, сами сделали override а сами закрылись.
	sealed class ChildAccount : BankAccount
	{
		// sealed закрыть выведение и наследование
		// функции базового класса вызывается через ключит base.
		// Из struct выводиться нельзя по default
		public sealed override int GetData()
		{
			// отработала базовай функция и выводимая, если это нужно.
			int rse = base.GetData();
			Data = rse - 333;
			return Data;
		}
	}
	class Program
	{
		static void Main(string[] args)

		{   // Member heen
			BankAccount b = new ChildAccount();
			var result = b.GetData();
			Console.WriteLine(result);
			Console.ReadLine();
		}
	}

// Exception and try catch
  class LockOfMoney : Exception
	{
		public override string Message
		{
			get { return "LockOf Money"; }
		}
	}
	class Program
	{
		static void Buy(int amount)
		{
			if(1 == 1)
			{
				LockOfMoney lom = new LockOfMoney();
				throw lom;
			}
		}
		static void Main(string[] args)
		{
			try
			{
				Buy(333);
			}
			catch (LockOfMoney ex)
			{
			}
			catch (Exception ex)
			{
			}
			finally
			{
				// отрабатывает всегда не зависимо был exception или нет.
			}
		}
	}

  // Exception method
	static class MyLibary
	{
		// Если к переменной подставить this
		public static string MyFunction(this int x)
		{
			return "Hello from " + x;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			int xx = 333;

			var res = MyLibary.MyFunction(xx);
			// то вызов функции можно представить ниже.
			res = xx.MyFunction();

			Console.WriteLine();
		}
	}

// Exception method
	static class MyLibary
	{
		// Если к переменной подставить this
		// Если подставить неявную передачу в качестве параметров то функцию можно вызвать у любого типа данных
		public static string MyFunction<XXX>(this XXX x)
		{
			return "Hello from " + x;
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			int xx = 333;

			var res = MyLibary.MyFunction(xx);
			// то вызов функции можно представить ниже.
			res = xx.MyFunction();
			res = "Test".MyFunction(); // даже так

			Console.WriteLine();
		}
	}

  // Reading and Writing local Data
	// Манипуляция файловой системой

	class Program
	{
		static void Main(string[] args)
		{
			// Code Access Security 3.5 Net => 4.0 Net
			if(!Directory.Exists("MyDir"))
			Directory.CreateDirectory("MyDir");
			if (!File.Exists("MyDir\\My.txt"))
			{
				StreamWriter sw = File.CreateText("MyDir\\My.txt");
				sw.WriteLine("Hello! This my own file created with cmd");
				sw.Flush(); // сделает сброс буфера и сохраняет на диск
				sw.Close(); // делает сброс буфера и закрывает программу
			}
			StreamReader sr = File.OpenText("MyDir\\My.txt"); // прочитываем текст
			Console.WriteLine(sr.ReadToEnd());
			sr.Close(); // закрывает файл очищаем буфер

			// два симетричный класса
			DirectoryInfo di = new DirectoryInfo("MyDir"); // выделяем память и обьявялем
			foreach (FileInfo file in di.GetFiles())
			{
				FileStream fs = file.OpenRead(); // поток на чтение и у файла выбираем метод открыть для чтения
				StreamReader s = new StreamReader(fs); // конвертируем
				Console.WriteLine(s.ReadToEnd());
				sr.Close(); // закрывает файл очищаем буфер
			}

			Console.ReadLine();
		}
	}

•

	Serialize an object as binary.
 class Program
	{
		[Serializable] // ключ разрешение слива данных
		public class Customer
		{
			public int ID { get; set; }
			public string Name { get; set; }
			public string Address { get; set; }
		}
		static void Main(string[] args)
		{
			List<Customer> tbl = new List<Customer>()
			{
				new Customer() { ID = 333, Name="Bob", Address="London"},
				new Customer() { ID = 365, Name="Mary", Address="London"},
				new Customer() { ID = 321, Name="Anastasia", Address="Amsterdam"}
			};

			// Отправляем файл
			FileStream fs = new FileStream("Custoners.bin", FileMode.Create); // читаем поток, если нет то создать, если есть перезаписать.
			//fs.Write
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, tbl); // конвертирует и укладывает файлы
			fs.Close(); // сбрасывает буффер

			// Читаем файл, другай сторогна
			fs = new FileStream("Custoners.bin", FileMode.Open);
			bf = new BinaryFormatter();
			var customers = bf.Deserialize(fs) as List<Customer> ;
			fs.Close();
			foreach (var c in customers)
			{
			Console.WriteLine(c.Name + "\t" + c.Address);
			}
			Console.ReadLine();
		}
	}

•

	Serialize an object as XML.

 public class Program
	{
		[Serializable] // ключ разрешение слива данных
		public class Customer
		{
			[XmlAttribute("CustID")] public int ID { get; set; }
			[XmlElement("C")] public string Name { get; set; }
			public string Address { get; set; }
		}
		static void Main(string[] args)
		{
			List<Customer> tbl = new List<Customer>()
			{
			   new Customer() { ID = 333, Name="Bob", Address="London"},
			   new Customer() { ID = 365, Name="Mary", Address="London"},
			   new Customer() { ID = 321, Name="Anastasia", Address="Amsterdam"}
			};

			// Отправляем файл
			FileStream fs = new FileStream("Custoners.xml", FileMode.Create); // читаем поток, если нет то создать, если есть перезаписать.
			//fs.Write
			//BinaryFormatter bf = new BinaryFormatter();
			XmlSerializer bf = new XmlSerializer(typeof(List<Customer>));
			bf.Serialize(fs, tbl); // конвертирует и укладывает файлы
			fs.Close(); // сбрасывает буффер

			// Читаем файл, другай сторогна
			fs = new FileStream("Custoners.xml", FileMode.Open);
			//bf = new BinaryFormatter();
			bf = new XmlSerializer(typeof(List<Customer>));
			var customers = bf.Deserialize(fs) as List<Customer> ;
			fs.Close();
			foreach (var c in customers)
			{
			Console.WriteLine(c.Name + "\t" + c.Address);
			}
			Console.ReadLine();
		}
	}

 // Serialization
	// WCF Library

	•

	Describe the purpose of serialization, and the formats that the .NET Framework supports.

	•

	Create a custom type that is serializable.

	•

	Serialize an object as binary.

	•

	Serialize an object as XML.

	•

	Serialize an object as JSON.

public class Program
{
	[Serializable] // ключ разрешение слива данных
	public class Customer
	{
		[XmlAttribute("CustID")] public int ID { get; set; }
		[XmlElement("C")] public string Name { get; set; }
		public string Address { get; set; }
	}
	static void Main(string[] args)
	{
		List<Customer> tbl = new List<Customer>()
			{
			   new Customer() { ID = 333, Name="Bob", Address="London"},
			   new Customer() { ID = 365, Name="Mary", Address="London"},
			   new Customer() { ID = 321, Name="Anastasia", Address="Amsterdam"}
			};

		// Отправляем файл
		FileStream fs = new FileStream("Custoners.xml", FileMode.Create); // читаем поток, если нет то создать, если есть перезаписать.
																		  //fs.Write
																		  //BinaryFormatter bf = new BinaryFormatter();
		XmlSerializer bf = new XmlSerializer(typeof(List<Customer>));
		bf.Serialize(fs, tbl); // конвертирует и укладывает файлы
		fs.Close(); // сбрасывает буффер

		// Читаем файл, другай сторогна
		fs = new FileStream("Custoners.xml", FileMode.Open);
		//bf = new BinaryFormatter();
		bf = new XmlSerializer(typeof(List<Customer>));
		var customers = bf.Deserialize(fs) as List<Customer>;
		fs.Close();
		foreach (var c in customers)
		{
			Console.WriteLine(c.Name + "\t" + c.Address);
		}
		Console.ReadLine();
	}
}

//Creating and Using Entity Data Models

	//Use the ADO.NET Entity Data Model Tools
	/*
	 * ADO.NET includes three xml files
	 * CSDL => C# classes
	 * MSL
	 * SSDL => DATABASE
	 *
	 *   public class Program
	{
		static void Main(string[] args)
		{
			ModelContainer ctx = new ModelContainer();

			Customer c1 = new Customer() { Id = 0, Name = "Bob", Address = "London" };
			Product p1 = new Product() { Id = 0, Name = "Mary", Price = 3.14 };
			Order o1 = new Order() { Customer = c1, Product = p1, Amount = 3 };

			Customer c2 = new Customer() { Id = 0, Name = "Milk", Address = "Miami" };
			Product p2 = new Product() { Id = 0, Name = "Wayn", Price = 2.8 };
			Order o2 = new Order() { Customer = c2, Product = p2, Amount = 3 };

			Customer c3 = new Customer() { Id = 0, Name = "Jon", Address = "Tyumen" };
			Order o3 = new Order() { Customer = c3, Product = p1, Amount = 5 };

			ctx.Customers.Add(c1);
			ctx.Customers.Add(c2);
			ctx.Customers.Add(c3);

			ctx.Product.Add(p1);
			ctx.Product.Add(p2);

			ctx.Orders.Add(o1);
			ctx.Orders.Add(o2);
			ctx.Orders.Add(o3);

			var x = new { Id = 333, Data = "xx", Price = 3.14 }; // анонимные типы данных

			// LINQ = LAMBDA EXPRESSION
			var query = from o1 in ctx.Orders
						where o1.Product.Name == "Bread"
						orderby o1.Id
						select new
						{ CustomerName = o1.Customer.Name,
							ProductName = o1.Product.Name,
							OrderAmount = o1.Amount};

			foreach (var item in query)
			{
				Console.WriteLine(item.CustomerName + "\t" + item.ProductName + "\t" + item.OrderAmount);
			}

			var query2 = ctx.Orders.Where(o1 => o1.Product.Name == "Bread").OrderBy( o1=> o1.Id).Select(o1 => o1);

			foreach (var item in query2)
			{
				Console.WriteLine(item.Customer.Name + "\t" + item.Customer.Address + "\t" + item.Product.Name + "\t" + item.Amount);
			}

			var orders = query2.ToList();

			orders[0].Customer.Address = "London";

			ctx.SaveChanges();

			Console.ReadLine()
		}
	}v

// Accessong Remote Data

	 *
	 * After completing this module, you will be able to:
	 * Send data to and receive data from web services and other remote data sources.
	 * Access data by using WCF Data Services.
	 *

public class Program
{
	static void Main(string[] args)
	{
		WebRequest request = WebRequest.Create("http://www.rambler.ru"); // схема протокола, сомтри какой обработчик зарегистирован, должны уметь распоковывать упаковывать стандартным спосоом, должны отправлять принимать данные.
		request.Method = "GET";

		request.Credentials = new NetworkCredential("Bob", "{Pa$$w0rd");
		HttpWebRequest httpReq = request as HttpWebRequest;

		// httpReq.ClientCertificates.Add добавляем сертификаты, выбираем чем представидться

		WebResponse response = request.GetResponse();
		var s = response.GetResponseStream(); // Байтовый поток, писать читатьб по байтно и конвертировать.
		StreamReader sf = new StreamReader(s); // Конвертация данных
		Console.WriteLine(sf.ReadToEnd());
		Console.ReadLine();
	}
}

			// Server
			ServiceHost svc = new ServiceHost(typeof(Service1));
			svc.Open();

			Console.WriteLine("Server is ready!!!");

			Console.ReadLine();

			svc.Close();

			Client
			MyServices.Service1Client svc = new MyServices.Service1Client();
			var customers = svc.DoWork(333);
			foreach (var item in customers)
			{
				Console.WriteLine(item.Name + "\t" + item.Address);
			}

			Console.ReadLine();

	public interface Service1 : IService1
	{
		public List<Customer> DoWork(int id)
		{
			CustomerDBEntities ctx = new CustomerDBEntities();
			ctx.Customers.ToList();
			var customers = from с in ctx.Customers.Local
							select new Customer() { Id = c.Id, Name = c.Name, Address = c.Address };

			//ctx.Orders.ToList();
			//ctx.Orders.ToList();

			return customers.ToList();
		}
	}

<==================================== MS DAY 4 ==========================================>

Designing the User Interface for a Graphical Applicatin

Button btn = new Button();
grd.AppenChild.Add(btn);
btn.Content = "Hello!"

namespace wpfApplication2
{
	public class MyControl : FrameworkElement / usercontrol / control
	{
		ovveride void OnRender(System.Windows.Media.DrawingContext drawingContext)
		{
			drawingContext.DrawEllipse(new SolidColorBrush(Color.FromRgb(0,255,0)), new Pen(Color.FromRgb(0,255,0), 2), new Point (this.Width / 2, this.Height / 2), Widht, Height)
		}
	}

//

	public class MyControl : Control
	{
		ovveride void OnRender(System.Windows.Media.DrawingContext drawingContext)
		{
			drawingContext.DrawEllipse(new SolidColorBrush(Color.FromRgb(0,255,0)), new Pen(Color.FromRgb(0,255,0), 2), new Point (this.Width / 2, this.Height / 2), Widht, Height)
		}
	}

	public Class MyDB
	{
		public MyDB()
		{
		}
		public string Date {get; set;}
	}
}

	// Performing Operations Asynchronously

	Asynchronous operations are closely related to tasks. The .NET Framework 4.5 includes some new features that make it easier to perform asynchronous operations. These operations transparently create new tasks and coordinate their actions, enabling you to concentrate on the business logic of your application. In particular, the async and await keywords enable you to invoke an asynchronous operation and wait for the result within a single method, without blocking the thread.
	 *
	 *
	 *

	public delegate string MyDelegate(int id);

public class Program
{
	public static string MyFunction(int x)
	{
		return "Hello!";
	}
	static void Main(string[] args)
	{
		MyDelegate d = new MyDelegate(MyFunction);
		var result = d.Invoke(323);

		Func<int, string> d2 = new Func<int, string>(MyFunction);
		result = d2.Invoke(323);

		Func<int, string> d3 = MyFunction;
		result = d3.Invoke(323);

		Func<int, string> d4 = delegate (int x)
		{ // ANONIMOUSE FUNCTION
			return "Hello!";
		};
		result = d4.Invoke(323);

		Func<int, string> d5 = (x) =>
		{ // ANONIMOUSE FUNCTION
			return "Hello!";
		};
		result = d5.Invoke(323);

		// LAMBDA EXPRESSION
		Func<int, string> d6 = (x) => "Hello!" + x;
		result = d6.Invoke(323);
	}
}

 // Performing Operations Asynchronously

	public class Program
	{
		public static string MyFunction(int id)
		{
			Thread.Sleep(10000);
			return "Hello from id= " + id;
		}
		static void Main(string[] args)
		{
			//Console.WriteLine(myFunction(333));
			//Console.WriteLine(myFunction(555));
			//Console.WriteLine(myFunction(777));

			var task1 = new Task<string>(() => MyFunction(333));
			var task2 = new Task<string>(() => MyFunction(555));
			var task3 = new Task<string>(() => MyFunction(777));
			task1.Start();
			task2.Start();
			task3.Start();

			Task[] tsk = new Task[3] {task1, task2, task3 };
			Task.WaitAll(tsk);

			Console.WriteLine(task1.Result);
			Console.WriteLine(task2.Result);
			Console.WriteLine(task3.Result);

			Console.ReadLine();
		}
	}

// тоже самое но короче

var task1 = Task.Run(() => MyFunction(333));
var task2 = Task.Run(() => MyFunction(555));
var task3 = Task.Run(() => MyFunction(888));

Task[] tsk = new Task[3] {task1, task2, task3 };
Task.WaitAll(tsk);

Console.WriteLine(task1.Result);
Console.WriteLine(task2.Result);
Console.WriteLine(task3.Result);

Console.ReadLine();

 public static string MyFunction(int id)
{
	Thread.Sleep(1);
	return "Hello from thread# " + Thread.CurrentThread.ManagedThreadId + " id:" + id;
}
static void Main(string[] args)
{
	Console.WriteLine("Main " + Thread.CurrentThread.ManagedThreadId);

	var task1 = Task.Run(() => MyFunction(333));
	var task2 = Task.Run(() => MyFunction(555));
	var task3 = Task.Run(() => MyFunction(888));

	Task[] tsk = new Task[3] {task1, task2, task3 };
	Task.WaitAll(tsk);

	int[] todo = new int[] { 333, 555, 888, 999, 222, 444, 644, 111 };

	//Parallel.ForEach(todo, (t) => MyFunction(t));
	// Parallel LINQ
	var query = from t in todo.AsParallel().WithDegreeOfParallelism(4)
		select MyFunction(t);

	var result = query.ToList();

	foreach (var r in result)
	{
		Console.WriteLine(r);
	}

	//Console.WriteLine(task1.Result);
	//Console.WriteLine(task2.Result);
	//Console.WriteLine(task3.Result);

	Console.ReadLine();
}

 // Parallel

public class Program
{
	public static string MyFunction(int id)
	{
		Thread.Sleep(1);
		return "Hello from thread# " + Thread.CurrentThread.ManagedThreadId + " id:" + id;
	}

	public static string MyFunction(int id, CancellationToken token)
	{
		for (int i = 0; i < 100; i++)
		{
			if (token.IsCancellationRequested)
				throw new Exception("My Error");
			token.ThrowIfCancellationRequested();
			Thread.Sleep(2000);
		}
		return "Hello from thread# " + Thread.CurrentThread.ManagedThreadId + " id:" + id;
	}
	static void Main(string[] args)
	{
		Console.WriteLine("Main " + Thread.CurrentThread.ManagedThreadId);

		var task1 = Task.Run(() => MyFunction(333)).ContinueWith<string>((t) => MyFunction(888));

		CancellationTokenSource ts = new CancellationTokenSource();

		var task2 = Task.Run(() => MyFunction(555, ts.Token);

		Console.ReadLine();
		ts.Cancel(true);

		Task[] tsk = new Task[2] {task1, task2 };
		Task.WaitAll(tsk);

		int[] todo = new int[] { 333, 555, 888, 999, 222, 444, 644, 111 };

		//Parallel.ForEach(todo, (t) => MyFunction(t));
		// Parallel LINQ
		var query = from t in todo.AsParallel().WithDegreeOfParallelism(4)
			select MyFunction(t);

		var result = query.ToList();

		foreach (var r in result)
		{
			Console.WriteLine(r);
		}

		//Console.WriteLine(task1.Result);
		//Console.WriteLine(task2.Result);
		//Console.WriteLine(task3.Result);

		Console.ReadLine();
	}
}

 // Performing Operations Asynchronously
	// Parallel Многопоточность

	public class Program
	{
		static int count = 0;
		static void MyFunctiony()
		{
			int tmp = count;
			tmp++;
			Thread.Sleep(1000);
			count = tmp;

			//count++;
		}
		static void Main(string[] args)
		{
			Task[] tsk = new Task[]
			{
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony()),
				Task.Run(() => MyFunctiony())
			};
			Task.WaitAll(tsk);

			Console.WriteLine(count);
			Console.ReadLine();
		}
	}

<==================================== MS DAY 5 ==========================================>

 // Integrating with Unmanaged Code
	// Creating and Using Dynamic Objects
	//
	// COM -> precompiled to CPU code (x86)
	// COM -> rigistry in OS -> GUID Global unity indentify -> ole32.dll
	// COM -> metadata C++ *.h (прототипы функции) -> IDL -> dll(tlb)
	//
	// Void* pointer -> vtbl
	// Unknow size -> How to free memory ??
	//
	//Tlbimp.exe -> idl (*.h) -> idl => class(.Net wrapper)
	// RCW class (RunTime Call Wrapper)
	// RCW лучше загружать готовые и не самопальные

	public class Program
	{   [Guid("0002DF01-0000-0000-C000-000000000046")]
		class MyIE
		{
		}
		static void Main(string[] args)
		{
		   // SHDocVw.InternetExplorer ie = new SHDocVw.InternetExplorer(); // RCW

			dynamic ie = new MyIE();
			ie.Visible = true;
			ie.Navigate("http://www.yandex.ru");
			ie.MyFunction("Hello");

			Marshal.ReleaseComObject(ie);

			Console.ReadLine();
		}
	}

// Integrating with Unmanaged Code
	// Managing the Lifetime of Objects and Controlling Unmanaged Resources

	class MyClass : IDisposable
	{
		public int data;
		~MyClass()
		{
			Console.WriteLine("Finalise thread: " + Thread.CurrentThread.ManagedThreadId);
			Save();
		}

		public void Save()
		{
			Marshal.ReleaseComObject
			GC.SuppressFinalize(this);
		}
		public void Dispose()
		{
			Save();
		}
	}
	struct MyStruct
	{
		public int data;
	}
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Finalise thread: " + Thread.CurrentThread.ManagedThreadId);

			MyClass clsTest = null;

			try
			{
				{ // stack
					MyClass cls;
					using cls = new MyClass(); // heap allocation
					{
						MyStruct strc = new MyStruct(); // stack allocation
						int i = 0; // stack allocation

						i = 333 / i;

						clsTest = cls;

						cls.Save();

						Marshal.ReleaseComObject(cls);
					}
				}// automatic clear stack
			}
			catch { }

			clsTest = null;

			GC.Collect(); // break app-> stacks analys and clear
		}
	}

 // Integrating with Unmanaged Code
	// Examining Object Metadata
	// для динамического взаимодействия с другими библиотеками
	// Расширяемость серверов
	// REFLECTION

namespace myLib
{
	[MyInterfaces.My("This is my class")]
	public class Class1 : MyInterfaces.IMyInterface
	{
		[MyInterfaces.My("This is MyFunction. Must be in transaction")]
		public string MyFunction()
		{
			return "Hello MyLib";
		}

		[PrincipalPermission(SecurityAction.Demand,Role ="Managers")]
		public string TestAttributeFunction()
		{
			return "Hello from myLib";
		}
	}
}

namespace AshtonBro.CodeBlog._2
{
	public class MyInterfaces
	{
		public interface IMyInterfaces
		{
			string MyFunction();

			string TestAttributeFunction();
		}

		[AttributeUsage(AttributeTargets.All)]
		public class MyAttribute : Attribute
		{
			public MyAttribute()
			{
			}
			public MyAttribute(string data)
			{
				Data = data;
			}
			public string Data { get; set; }
		}
	}
}

   static void Main(string[] args)
		{
			Assembly asm = Assembly.LoadFile(Path.GetFullPath("myLib.dll"));

			foreach (var item in asm.GetTypes())
			{
				Console.WriteLine(item.FullName);

				foreach (var attr in item.GetCustomAttribute())
				{
					MyInterfaces.MyAttribute a = attr as MyInterfaces.MyAttribute;
					if (a != null)
					{
						Console.WriteLine(a.Data);
					}
				}

				foreach (MethodInfo mi in item.GetMethods())
				{
					Console.WriteLine(mi.Name);
				}
			}

			Type t = asm.GetType("MyLib.MyClass");

			MethodInfo method = t.GetMethod("MyFunction");
			//method.GetMethodBody();

			//object o = Activator.CreateInstance(t);
			//object result = method.Invoke(o, new object[]{ });

			var o = Activator.CreateInstance(t) as MyInterfaces.IMyInterfaces;

			Console.WriteLine(o.TestAttributeFunction());
		   // Console.WriteLine(o.MyFunction());
			// Console.WriteLine(result.ToString());

			Console.ReadLine();
		}
	}

PRIVATE ASSEMBLY --> NAME

STRONG NAME ASSEMBLY -> NAME + CRYPTO HASH + Version

public class Program
{
	static void Main(string[] args)
	{
		var unit = new CodeCompileUnit();
		var ns = new CodeNamespace("MyOrgGazprom"); // create namespace
		unit.Namespaces.Add(ns);
		ns.Imports.Add(new CodeNamespaceImport("System")); // add using System;
		var cls = new CodeTypeDeclaration("MyClass"); // Create new MyClass
		ns.Types.Add(cls);
		var main = new CodeEntryPointMethod(); // получили функцию static void Main
		cls.Members.Add(main);

		var cs = new CSharpCodeProvider();

		var file = File.CreateText("MyProg.cs");

		var writer = new IndentedTextWriter(file);

		var options = new CodeGenerationOptions();

		cs.GenerateCodeFromCompileUnit(unit, writer, options);

		writer.Close();
	}
}

	// Implementing Symmetric Encryption
	// Implementing Asymmetric Encryption
		{
			var data = "Test stream";
			SymmetricAlgorithm alg = new AesManaged(); // TripleDESCryptoServiceProvider
			byte[] key = alg.Key;
			byte[] IV = alg.IV;

			// ШИФРОВКА
			FileStream fs = new FileStream("my.bin", FileMode.Create);

			CryptoStream cs = new CryptoStream(fs, alg.CreateEncryptor(), CryptoStreamMode.Write);

			StreamWriter sw = new StreamWriter(cs);

			sw.Write(data);
			sw.Close();
			cs.Flush();
			cs.Close();
			fs.Close();

			alg = new AesManaged();
			alg.KeySize = 256;
			alg.Key = key;
			alg.IV = IV;

			// ДЕШИВРОВКА
			fs = new FileStream("my.bin", FileMode.Open);

			cs = new CryptoStream(fs, alg.CreateDecryptor(), CryptoStreamMode.Read);

			StreamReader sr = new StreamReader(cs);

			Console.WriteLine(sr.ReadToEnd());
			Console.ReadLine();
		}

	// Encrypting and Decrypting Data
	// Implementing Symmetric Encryption
	// Implementing Asymmetric Encryption
	public class Program
	{
		static void Main(string[] args)
		{
			string data = "Hello RSA!";

			// collision resistance

			HMACSHA1 hashalg = new HMACSHA1();
			byte[] hash = hashalg.ComputeHash(Encoding.UTF8.GetBytes(data));

			RSACryptoServiceProvider alg = new RSACryptoServiceProvider(); // Определит количетсво byte

			string pubPrivateKey = alg.ToXmlString(true);
			string PubKey = alg.ToXmlString(false);

			byte[] byteData = Encoding.UTF8.GetBytes(data);
			var encryptedData = alg.Encrypt(byteData, true);

			alg = new RSACryptoServiceProvider();
			alg.FromXmlString(pubPrivateKey);
			byteData = alg.Decrypt(encryptedData, true);
			Console.WriteLine(Encoding.UTF8.GetString(byteData));

			Console.ReadLine();
		}
	}

// Types of user
	public enum Role { Teacher, Student };

	public struct Grade
	{
		public int StudentID { get; set; }
		public string AssessmentDate { get; set; }
		public string SubjectName { get; set; }

		public string Assessment { get; set; }
		public string Comments { get; set; }
	}

	public struct Student
	{
		public int StudentID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public int TeacherID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}


	public struct Teacher
	{
		public int TeacherID { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Class { get; set; }
	}

		public event EventHandler LogonFailed;

		#endregion

		#region Logon Validation

		private void Logon_Click(object sender, RoutedEventArgs e)
		{
			var teacher = (from Teacher t in DataSource.Teachers
						   where String.Compare(t.UserName, username.Text) == 0 && String.Compare(t.Password, password.Password) == 0
						   select t).FirstOrDefault();

			if(!String.IsNullOrEmpty(teacher.UserName))
			{
				SessionContext.UserID = teacher.TeacherID;
				SessionContext.UserRole = Role.Teacher;
				SessionContext.UserName = teacher.UserName;
				SessionContext.CurrentTeacher = teacher;

				LogonSuccess(this, null);
				return;
			}
			else
			{
				var student = (from Student s in DataSource.Students
							   where String.Compare(s.UserName, username.Text) == 0 && String.Compare(s.Password, password.Password) == 0
							   select s).FirstOrDefault();

				if(!String.IsNullOrEmpty(student.UserName))
				{
					SessionContext.UserID = student.StudentID;
					SessionContext.UserRole = Role.Student;
					SessionContext.UserName = student.UserName;
					SessionContext.CurrentStudent = student;

					LogonSuccess(this, null);
					return;
				}
			}

			LogonFailed(this, null);
		}

	private void Refresh()
		{
			switch (SessionContext.UserRole)
			{
				case Role.Student:

					txtName.Text = String.Format("Welcome {0} {1} !", SessionContext.CurrentStudent.FirstName, SessionContext.CurrentStudent.LastName);
					// Display the details for the current student
					GotoStudentProfile();
					break;

				case Role.Teacher:
					txtName.Text = String.Format("Welcome {0} {1} !", SessionContext.CurrentTeacher.FirstName, SessionContext.CurrentTeacher.LastName);
					// Display the list of students for the teacher
					GotoStudentsPage();
					break;
			}
		}

	private void Logon_Failed(object sender, EventArgs e)
		{
		   MessageBox.Show("The " + logonPage.username.Text + " must try again", "Logon Faild", MessageBoxButton.OK, MessageBoxImage.Error);
		}
ArrayList students = new ArrayList();
			var teacher = SessionContext.CurrentTeacher.TeacherID;

			foreach (Student student in DataSource.Students)
			{
				if(student.TeacherID == teacher)
				{
					students.Add(student);
				}
			}

			list.ItemsSource = students;

			txtClass.Text = String.Format("Class {0}", SessionContext.CurrentTeacher.Class);

		#region Event Members
		public delegate void StudentSelectionHandler(object sender, StudentEventArgs e);
		public event StudentSelectionHandler StudentSelected;
		#endregion

		#region Event Handlers

		private void Student_Click(object sender, RoutedEventArgs e)
		{
			Button studentClicked = sender as Button;
			if(studentClicked != null)
			{
				int studentInd = (int)studentClicked.Tag;
				if(StudentSelected != null)
				{
					Student student = (Student)studentClicked.DataContext;
					StudentSelected(sender, new StudentEventArgs(student));
				}
			}
		}
		#endregion

			studentName.DataContext = SessionContext.CurrentStudent;
			if (SessionContext.UserRole == Role.Student)
			{
				btnBack.Visibility = Visibility.Collapsed;
			}
			else
			{
				btnBack.Visibility = Visibility.Visible;
			}

			// find the grades
			ArrayList grades = new ArrayList();
			foreach (Grade grade in DataSource.Grades)
			{
				if(grade.StudentID == SessionContext.CurrentStudent.StudentID)
				{
					grades.Add(grade);
				}
			}

			studentGrades.ItemsSource = grades;

		 // Types of user
    public enum Role { Teacher, Student };

    // WPF Databinding requires properties

    public class Grade
    {
        public int StudentID { get; set; }
        public string AssessmentDate { get; set; }
        public string SubjectName { get; set; }
        public string Assessment { get; set; }
        public string Comments { get; set; }

        public Grade(int studentID, string assessmentDate, string subjectName, string assessment, string сomments)
        {
            this.StudentID = studentID;
            this.AssessmentDate = assessmentDate;
            this.SubjectName = subjectName;
            this.Assessment = assessment;
            this.Comments = сomments;
        }

        public Grade()
        {
            StudentID = 0;
            AssessmentDate = DateTime.Now.ToString("d");
            SubjectName = "Math";
            Assessment = "A-";
            Comments = "Good";
        }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }
        private string _password = " ";
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public bool VerifyPassword(string pass)
        {
            return (String.Compare(pass, _password) == 0);
        }
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentID, string userName, string password, int teacherID, string firstName, string lastName)
        {
            this.StudentID = studentID;
            this.UserName = userName;
            this.Password = password;
            this.TeacherID = teacherID;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Student()
        {
            StudentID = 0;
            UserName = String.Empty;
            Password = String.Empty;
            TeacherID = 0;
            FirstName = String.Empty;
            LastName = String.Empty;
        }
    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        private string _password = " ";
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public bool VerifyPassword(string pass)
        {
            return (String.Compare(pass, _password) == 0);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }

        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string _class)
        {
            this.TeacherID = teacherID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Class = _class;
        }

        public Teacher()
        {
            TeacherID = 0;
            UserName = String.Empty;
            Password = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            Class = String.Empty;
        }
    }

		 // Find the user in the list of possible users - first check whether the user is a Teacher
            var teacher = (from Teacher t in DataSource.Teachers
                           where String.Compare(t.UserName, username.Text) == 0
                           && t.VerifyPassword(password.Password)
                           select t).FirstOrDefault();

            // If the UserName of the user retrieved by using LINQ is non-empty then the user is a teacher
            if (teacher != null && !String.IsNullOrEmpty(teacher.UserName))
            {
                // Save the UserID and Role (teacher or student) and UserName in the global context
                SessionContext.UserID = teacher.TeacherID;
                SessionContext.UserRole = Role.Teacher;
                SessionContext.UserName = teacher.UserName;
                SessionContext.CurrentTeacher = teacher;

                // Raise the LogonSuccess event and finish
                LogonSuccess(this, null);
                return;
            }
            // If the user is not a teacher, check whether the username and password match those of a student
            else
            {
                var student = (from Student s in DataSource.Students
                               where String.Compare(s.UserName, username.Text) == 0
                               && s.VerifyPassword(password.Password)
                               select s).FirstOrDefault();

                // If the UserName of the user retrieved by using LINQ is non-empty then the user is a student
                if (student != null && !String.IsNullOrEmpty(student.UserName))
                {
                    // Save the details of the student in the global context
                    SessionContext.UserID = student.StudentID;
                    SessionContext.UserRole = Role.Student;
                    SessionContext.UserName = student.UserName;
                    SessionContext.CurrentStudent = student;

                    // Raise the LogonSuccess event and finish
                    LogonSuccess(this, null);
                    return;
                }

        private string _assessmentDate;
        public string AssessmentDate
        {
            get { return _assessmentDate;  }
            set
            {
                DateTime assessmentDate;
                if(DateTime.TryParse(value, out assessmentDate))
                {
                    if(assessmentDate > DateTime.Now)
                    {
                        throw new ArgumentOutOfRangeException("AssessmentDate error", "AssessmentDate must be lower then now time");
                    }
                    _assessmentDate = assessmentDate.ToString("d");
                }
                else
                {
                    throw new ArgumentException("AssessmentDate", "Assessment date is not recognized");
                }
            }
        }

        private string _subjectName;
        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                if(DataSource.Subjects.Contains(value))
                {
                    _subjectName = value;
                }
                else
                {
                    throw new ArgumentException("SubjectName", "SubjectName is not recognized");
                }
            }
        }

        private string _assessment;
        public string Assessment
        {
            get { return _assessment; }
            set
            {
                if(DataSource.Grades.Contains(value))
                {
                    _assessment = value;
                }
            }
        }

		  public int CompareTo(Student other)
        {
            string thisStudent = LastName + FirstName;
            string otherStudent = other.LastName + other.FirstName;
            return (String.Compare(thisStudent, otherStudent));
        }

		public static List<Student> Students;
			DataSource.Students.Sort();

	public void AddGrade(Grade grade)
        {
            if(grade.StudentID == 0)
            {
                grade.StudentID = StudentID;
            }
            else
            {
                throw new ArgumentException("Grade", "Grade to a different Student");
            }
        }



----------------------------------------------------
static void Main(string[] args)
{
	Console.ForegroundColor = ConsoleColor.Green;

	string provider = ConfigurationManager.AppSettings["provider"];
	string connectionString = ConfigurationManager.AppSettings["connectionString"];

	DbProviderFactory factory = DbProviderFactories.GetFactory(provider);

	using (DbConnection connection = factory.CreateConnection())
	{
		if (connection == null)
		{
			Console.WriteLine("Connection Error");
			Console.ReadLine();
			return;
		}

		connection.ConnectionString = connectionString;

		connection.Open();

		DbCommand command = factory.CreateCommand();

		if (command == null)
		{
			Console.WriteLine("Command Error");
			Console.ReadLine();
			return;
		}

		command.Connection = connection;

		command.CommandText = "Select * From Sales.Customers";

		using (DbDataReader dataReader = command.ExecuteReader())
		{
			while (dataReader.Read())
			{
				Console.WriteLine($"{dataReader["custId"]}" + $"{ dataReader["city"]}");
			}
		}
		Console.ReadLine();
	}

		}
 */
