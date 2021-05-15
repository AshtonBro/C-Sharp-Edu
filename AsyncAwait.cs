using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/*
 <----------- Асинхронность (async, await) и многопоточность (thread) в C# -----------------> 

    TODO: 1 - В своей предметной области создать метод со сложными вычислениями
    TODO: 1 - Сделать для этого метода обертку в виде async-метода
    TODO: 1 - Переписать свой код в асинхронном варианте
    TODO: 2 - Создать вручную поток (thread) 
    TODO: 2 - Сделать для него повышенный приоритет
    TODO: 2 - Запустить выполнение и попробовать завершить приложение
    TODO: 3 - Использовать lock
 */

namespace AshtonBro.Code
{
    public class AsyncAwait
    {
        public static void RunDemo()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var result = TaskMaanger.SaveFileAsync("stream.txt");
            var input = Console.ReadLine();
            Console.WriteLine(result.Result);
            Console.ReadLine();
        }

        public class TaskMaanger
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

            public static async Task<bool> SaveFileAsync(string path)
            {
                var result = await Task.Run(() => SaveFile(path));
                return result;
            }

            public static bool SaveFile(string path)
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
    }
}


