using System;
using System.Collections.Generic;
using System.Text;

/*
    <---------------------------- сборка мусора (Garbage Collection, GC) .NET в C# --------------------------------------->
    Ссылочные и значимые переменные
    Reference & Value
    Куча (В Куче храним большие классы и некоторые типы)  и Стек (Стек маленький, поэтому там хранится тривиальные типы)
    string													int 
    Class													char

    Garbage Collection необходимо чтобы очищать кучу от выделенного места для ссылочного типа данных

    GC.Collect(2); // чувак иди ка почисти нам память 
    (внутри скобок можно указать номер поколения с которого можно произвести очистку) 
 */

namespace AshtonBro.Code
{
    class GarbageCollection
    {
    }
    class myClass : IDisposable
    {
        public myClass() { } // Конструктор
        ~myClass() { } // Деструктор (Тут можно определить его поведение когда наш класс будет уничтожаться, особенно при работе с потоками) Деструктор вызывается системой 

        public void Dispose()
        {
            GC.Collect();
        }

        public static void RunDemo()
        {
            Console.WriteLine(GC.GetTotalMemory(false)); // Проверяем память до заполнения

            for (int i = 0; i < 10000; i++)
            {
                var obj = (object)i;
                int j = (int)obj;
            }

            Console.WriteLine(GC.GetTotalMemory(false)); // проверяем память после заполнения

            GC.Collect();

            using (var c = new myClass())
            {

            }

            Console.WriteLine(GC.GetTotalMemory(false)); // проверяем память после очистки заполненной памяти

            Console.ReadLine();
        }
    }
}
