using System;

namespace AshtonBro.Code
{
    class Student
    {
        public string Name { get; }
        public int Age { get; }
        public Group Group { get; set; }
        public Student(string name, int age)
        {
            // проверка входных параметров 

            Name = name;
            Age = age;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
