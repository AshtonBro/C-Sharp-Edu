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

        public static void RunDemo()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Person person = new Person("Evgenii", 18);
            Console.WriteLine(person.GetInfo());

            Person person2 = new Person("Max", 21);
            Console.WriteLine(person2.GetInfo());

            Printer.Printering("Yo this is printer!");
            Console.ReadLine();
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

/*
 
public class Person
{
    // Fields
    private string _name;


    // Properties

    // Method
    public void SetName(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return "From GetName: " + _name;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"From PrintInfo: {_name}");
    }
}

 */