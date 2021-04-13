using System;
using System.Linq;

/*
 <---------------------------- Атрибуты (Attribute) и Рефлексия (Reflection) .NET в C# --------------------------------------->

    TODO: Исследовать рефликсию
    TODO: Реализовать получение свойства, классов и методов
    TODO: Создать свой собственный атрибут
    TODO: Использовать свой собственный атрибут в классе

    Чтобы инициализировать Атрибут необходимо наследоваться от System.Attribute
    Атрибут на практике применяется идеально подходит и используется при Серилизации (перевести его из формата объекта в текстовый формат JSOX, XML...)
    Мы отмечает те классы которые ходим серилизовать и с помощью атрибутов отмечаем те свойства который для нас выжны во время серилизации.

    При ASP.NET мы указываем какой тип, предмет, тип запроса GET,SET - c помощью атрибута помечаем, это позволяет компилятору явно понимать к какому именно из методов с одинаковыми именами обращатся

 */

namespace AshtonBro.Code
{
    class AttributeReflection
    {
        public static void RunDemo()
        {
            var photo = new Photo("hello.png")
            {
                Path = @"C:\Users\solov\OneDrive\Изображения\222.png"
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


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)]
    // Используем атрибут AttributeTargets что присвоить к кокой структуре относить атрибут через точно можно
    // получить большой cписок структур

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

}

/*
    Чаще всего атрибуту задают без параметров, для того что была возможность задавать свойства с 
    помощью их имени и делаем объявление атрибута достаточно хорошо читаемый
    Если конструктор без параметров, то и атрибут мы можем создать пустой

    var type = type-of(Photo); // базовый класс type, который является контейнеров хранения информации о классе
 */