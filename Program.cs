using System;

namespace AshtonBro.CodeBlog._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            string s = "1";
            int i = s; // error
            int j = 5; 
            double d = j; // no error
            int k = d; // error

            byte b = 42; // Возможное значение от 0 до 255;
            int i = b; // возможное значение от -2миллиардов до 2миллиардов

        }
    }
}

/*
 * 
string UpperCamelCase; // PascalCase, first letter uppercase. (C#)
string lowerCameCase; // PascalCase, first letter lowercase. (C#)
string snake_case; // Snake style (JS)
string FAT_SNAKE_CASE; // Fat snake style, usually use for constant's
string kebab-case; // -#-#-#-#-- Kebab
string sHungarianCase; // Hungarian notation (C++)

<------------------------------------- Приведение и преобразование типов C#------------------------>

*/

