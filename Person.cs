using System;

namespace AshtonBro.Code
{
    public class Person
    {
        //Constructor
        public Person(string name, int age)
        {
            _name = name;
            Age = age;
        }
        public Person(int age)
        {
            Age = age;
        }

        public Person() { }

        // Fields
        private string _name;

        // Properties
        public string Name { get => _name; set => _name = value; }
        public int Age { get; private set; }

        // Method
        public string GetInfo()
        {
            return Name + ": " + Age;
        }
    }

    public static class Printer
    {
        public static void Printering(string value)
        {
            Console.WriteLine($"Print: {value}");
        }
        public static void Printering(int value)
        {
            Console.WriteLine($"Print: {value}");
        }
    }
}