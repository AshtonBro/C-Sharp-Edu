using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AshtonBro.Code
{
    /*
   <---------------------------- Индексаторы (Indexer) и Итераторы (yield). Интерфейс IEnumerable в C# --------------------------------------->
    Чтобы наш класс смог работать с циклом foreach нам нужно чтобы этот класс реализовывал interface IEnumerable
    Enumerator это Interface который возвращает нам коллекцию
    Iterator это переборщиц нашей коллекции

    Для того чтобы перебрать все элементы коллекции можно использовать yield
     */

    class Indexer
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"Авто: {Name}, Номер: {Number}";
        }

        public static void RunDemo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var cars = new List<Indexer>()
            {
                new Indexer() { Name = "Ford", Number = "A001AA01" },
                new Indexer() { Name = "Toyota", Number = "B021AD74" },
                new Indexer() { Name = "Shkoda", Number = "C231CA22" }
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
            parking[1] = new Indexer() { Name = "BMW", Number = num };
            Console.WriteLine(parking[1]);

            Console.ReadLine();
        }

    }


    /*
       Сделали метод для доступа по индексу.parking["B021AD74"]?.Name передали в качестве параметра в [] скобка номер машины, в классе Parking оно пришло в индексатор,
      через LINQ ищем номер в на нашей парковке в массиве и выдаём пользователю имя авто если оно есть или NULL в другом случае
     */

    class Parkink : IEnumerable
    {
        private List<Indexer> _cars = new List<Indexer>();
        private const int MAX_CARS = 100;

        public Indexer this[string number] // индексация, обращаемся к элементу по его индексу
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

        public Indexer this[int position]
        {
            get
            {
                if (position < _cars.Count)
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
        public int Add(Indexer car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Car is NULL");
            }

            if (_cars.Count < MAX_CARS)
            {
                _cars.Add(car);
                return _cars.Count - 1;
            }

            return -1;
        }

        public void GoOut(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Number is NULL or empty");
            }

            var car = _cars.FirstOrDefault(c => c.Name == number);
            if (car != null)
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

    //public Car this[string number] // индексация, обращаемся к элементу по его индексу
    //{
    //    get
    //    {
    //        var car = _cars.FirstOrDefault(c => c.Number == number);
    //        return car;
    //    }
    //    //set
    //    //{

    //    //}
    //}


    /*
        public тип this[тип индекс] // индексация, обращаемся к элементу по его индексу
       {
           get
           {

           }
           set
           {

           }
       }
     */

}
