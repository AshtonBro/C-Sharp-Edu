using System;

namespace AshtonBro.Code
{
    public class Person
    {
        // Fields
        private string _name;
        private int _age;

        // Properties
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 18)
                {
                    _age = -1;
                }
                else
                {
                    _age = value;
                }
            }
        }

        public string GetName()
        {
            return _name ?? "Empty";
        }

        // Properties
        public void SetName(string value)
        {
            _name = value;
        }

        // Method

        public void PrintInfo()
        {
            Console.WriteLine($"From PrintInfo: {_name}");
        }
    }
}