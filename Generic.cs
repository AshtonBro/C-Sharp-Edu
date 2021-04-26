using System;
using System.Collections.Generic;

namespace AshtonBro.Code
{
    public class Generic
    {
        public static void RunDemo()
        {
            Room<int> intRoom = new Room<int>(10);
            intRoom.Place(2); 
            
            for (int i = 0; i < intRoom.Count; i++)
            {
                Console.WriteLine(intRoom.Get(i));
            }

            Room<string> stringRoom = new Room<string>(10);
            stringRoom.Place("Wall");

            for (int i = 0; i < stringRoom.Count; i++)
            {
                Console.WriteLine(stringRoom.Get(i));
            }

            Room<Person> personRoom = new Room<Person>(10);
            personRoom.Place(new Person("Roman", 32));
            personRoom.Place(new Person("Evgenii", 28));

            for (int i = 0; i < personRoom.Count; i++)
            {
                Console.WriteLine(personRoom.Get(i));
            }

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            numbers.Add(10);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Room<string> roomAr1 = new Room<string>(10);
            Comparer<string, int> comparerAr2 = new Comparer<string, int>();

            Console.WriteLine(roomAr1);
            Console.WriteLine(comparerAr2);

            Apartment apartment = new Apartment();
            var numberT = apartment.Place<int>(23);
            Console.WriteLine(numberT);

            IRoom<int> bathRoomInt = new BathRoomInt();
            int bathInt = bathRoomInt.Place(18);
            Console.WriteLine(bathInt);

            IRoom<Person> bathRoom = new BathRoom<Person>();
            Person person = bathRoom.Place(new Person("Max", 30));
            var info = person.GetInfo();

            Console.WriteLine(info);



            Console.ReadLine();
        }
    }

    //Ковариантность контрвариантность 
    // Относится только к обобщенныеинтерфейсам

    public interface IRoom
    {
        void Place(object gameObject);
        void Place<T>(T gameObject);
    }

    public interface IRoom<T>
    {
        T Object { get; }
        T Place(T gameObject);
    }

    //public interface IRoom<in T>
    //{
    //    T Object { set; }
    //    void Place();
    //}


    //public interface IRoom<out T>
    //{
    //    T Object { get; }
    //    void Place(T value);
    //}

    public class BedRoom : IRoom
    {
        public void Place(object gameObject)
        {
            throw new NotImplementedException();
        }

        public void Place<T>(T gameObject)
        {
            throw new NotImplementedException();
        }
    }

    public class BathRoomInt : IRoom<int>
    {
        public int Object { get; private set; }

        public int Place(int gameObject)
        {
            Object = gameObject;
            return Object;
        }
    }

    public class BathRoom<T> : IRoom<T>
    {
        public T Object { get; private set; }

        public T Place(T gameObject)
        {
            Object = gameObject;
            return Object;
        }
    }

    public class Pit<T> : IRoom<T>
    {
        public T Object { get; private set; }

        public T Place(T gameObject)
        {
            Object = gameObject;
            return Object;
        }

        public Person Place(Person person)
        {
            person.IsAlive = false;
            return person;
        }
    }

    // Ограничение обобщений where [параметр] : [тип данных, структуры или конкретный класс]
    //public class Room<T> where T : Person
    public class Room<T>
    {
        private T[] _objects;

        public Room(int capacity)
        {
            _objects = new T[capacity];
            Count = 0;
        }

        public int Count { get; set; }

        public T[] Objects
        {
            get
            {
                return _objects;
            }
        }

        public void Place(T gameObject)
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                if(_objects[i].Equals(default(T)))
                {
                    _objects[i] = gameObject;
                    Count++;
                    break;
                }
            }
        }

        public void Place<V>(V item) where V : Person
        {
            Console.WriteLine(item.Name);
        }

        public T Get(int index)
        {
            return _objects[index];
        }
    }
    
    public class Apartment
    {
        public T Place<T>(T value)
        {
            return value;
        }

        public string Place(string value)
        {
            return value;
        }
    }

    // для интерфейсов синтаксис ограничения

    // public class Comparer<TSource, TDestination>
    // where TSource : Person
    // where TDestination : Person
    public class Comparer<TSource, TDestination>
    {

    }

}
