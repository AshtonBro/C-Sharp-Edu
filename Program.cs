using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Threading.Tasks;

namespace AshtonBro.CodeBlog._2
{
    class Customer // Reference Type
    {
        public int id;

    }
    struct Order
    {
        public int amount;
    }
    class Product
    {
        public double Price;
    }
    class Program
    {
        // csc.exe => IL -> compile taget CPU + OS

       
        static void myFunc(Order o)
        {
            o.amount = 100;
        }
        static void Main(string[] args)
        {
            Order or = new Order();
            or.amount = 0;
            myFunc(or);
            Console.WriteLine(or.amount);

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



            Console.ReadLine();
        }

    }
}

/*
 
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
/*
* Много 
* строчный
* коментарий 
*
*/

/*

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
*/

