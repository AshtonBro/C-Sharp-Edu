using System;
using System.Collections.Generic;
using System.Text;

namespace AshtonBro.Code
{
    class Generic
    {
    }

    public class Room<T>
    {
        private T[] _objects;

        public Room(int capacity)
        {
            _objects = new T[capacity];
        }

        public int Count { get; set; }

        public void Place(T gameObject)
        {

        }
    }
    
    public class Comparer<TSource, TDestination>
    {

    }
}
