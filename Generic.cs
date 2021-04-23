using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }

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
                if(_objects[i] == null)
                {
                    _objects[i] = gameObject;
                    Count++;
                    break;
                }
            }
        }

        public T Get(int index)
        {
            return _objects[index];
        }
    }
    
    public class Comparer<TSource, TDestination>
    {

    }
}
