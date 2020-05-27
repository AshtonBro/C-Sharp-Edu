using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.IO;

namespace AshtonBro.CodeBlog._2
{
    // Reading and Writing local Data
    // Манипуляция файловой системой
   
    class Program
    {
        static void Main(string[] args)
        {
            // Code Access Security
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
*/

