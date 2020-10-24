using System;

namespace AshtonBro.Code
{

	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;



			Console.ReadLine();
		}

	}
}


/*


<--------------------------------------------------------ЗАМЕТКИ И ПРИМЕРЫ С КНИГИ: C# 4.0 полное руководство Герберт Шилд
----- Method Do While()
int num, nextdigit;
num = 19458;
Console.WriteLine($"Число: {num}");
Console.Write("Чисто в обратном порядке: ");
do
{
	nextdigit = num % 10;
    Console.Write(nextdigit);
	num = num / 10;
} while (num > 0);
------ Metod While ()
int e;
int result;
for (int i = 0; i < 10; i++)
{
	result = 1;
	e = i;
	while (e > 0)
    {
		result *= 2;
		e--;
    }
    Console.WriteLine("2 в степени " + i + " равно " + result);
}
------ Метод Math.Sqrt() 
double n;
for (n = 1.0; n <= 10; n++)
{
    Console.WriteLine("Квавадратный корень из {0} равен {1}", n, Math.Sqrt(n));
	Console.WriteLine("Целая часть числа: {0}", (int) Math.Sqrt(n));
	Console.WriteLine("Дробная часть числа: {0}", Math.Sqrt(n) - (int)Math.Sqrt(n));
}
------- Выполнить деление на целое и не целове значение
int j, d, n;
j = 10;
d = 2;
for (n = 1; n < 10; n++)
{
	if (d != 0 && (j % d) == 0)
		Console.WriteLine(n + " делится нацело на " + d);
	d = 0; // задать нулевое значение переменной d
	// d равно нулю, поэтому второй операнд не вычисляется 
	if (d != 0 && (n % d) == 0)
		Console.WriteLine(n + " делится нацело на " + d);
	// Если теперь попытаться сделать то же самое без укороченного 
	// логического оператора, то возникнет ошибка из-за деления на нуль.
	if (d != 0 & (n % d) == 0)
		Console.WriteLine(n + " делится нацело на " + d);
}
----Проверить на положительно или отрицательное число
decimal price;
decimal discount;
decimal discount_price;
// рассчитать цену со скидкой
price = 19.95m;
discount = 0.15m;
discount_price = price - (price * discount);
Console.WriteLine("Цена со скидкой: {0:C}", discount_price);
int i;
for (i = -5; i <= 5; i++)
{
    Console.Write("Проверка " + i + ": ");
	if (i < 0)
	{
		Console.WriteLine("Отрицательное число");
	}
	else`
    {
		Console.WriteLine("Положительное число");
	}
}

*/