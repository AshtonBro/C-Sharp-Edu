using System;

/*
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

 */

namespace AshtonBro.Code
{
    class AnonymousType
    {
        public static void RunDemo()
        {
            var i = 5;

            var parking = new
            {
                Name = "BMW",
                Number = "R435RT72",
                Power = 234
            };

            Console.WriteLine(parking);
            Console.WriteLine($"Name: {parking.Name} Number: {parking.Number} Power: {parking.Power}");

            var car = new Indexer()
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

    public class Car
    {
        public string Name { get; set; }
    }
}
