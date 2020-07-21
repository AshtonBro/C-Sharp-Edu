using System;
using System.Runtime.Serialization;

namespace AshtonBro.Code
{
    [DataContract]
    class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
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
