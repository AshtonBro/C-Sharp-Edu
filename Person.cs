using System;

namespace AhstonBro.Code
{
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
}