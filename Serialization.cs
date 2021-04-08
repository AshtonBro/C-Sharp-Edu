using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace AshtonBro.Code
{
    
//<---------------------------- Сериализация(serialization) объектов и работа с XML и JSON в C# 

    [Serializable]
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

    [Serializable]
    class Group
    {
        [NonSerialized]
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);

        private int privateInt;
        public int Number { get; set; }
        public string Name { get; set; }
        public Group()
        {
            Number = rnd.Next(1, 10);
            Name = "Group: " + rnd;
        }

        public void SetPrivate(int i)
        {
            privateInt = i;
        }

        public int GetPrivate()
        {
            return privateInt;
        }

        public Group(int number, string name)
        {
            // проверка входных параметров
            Number = number;
            Name = name;
        }
        public override string ToString()
        {
            return "Группа: " + Number.ToString(); // privateInt.ToString();
        }
    }

    class Serialization
    {
        //<------ JSONFormater------>
        public void JsonFormatter(List<Student> students)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));

            using (var file = new FileStream("students.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(file, students);
            }

            using (var file = new FileStream("students.json", FileMode.OpenOrCreate))
            {
                if (jsonFormatter.ReadObject(file) is List<Student> newStudents)
                {
                    foreach (var student in newStudents)
                    {
                        Console.WriteLine(student + " " + student.Group.GetPrivate());
                    }
                }
            }
        }

        //< ------XmlFormater------ >
        public void XmlFormater(List<Group> groups)
        {
            var xmlFormater = new XmlSerializer(typeof(List<Group>));

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                xmlFormater.Serialize(file, groups);
            }

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate))
            {
                if (xmlFormater.Deserialize(file) is List<Group> newGroups)
                {
                    foreach (var group in groups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
        }

        //< ------SoapFormatter------ >
        public void SOAPFormatter(List<Group> groups)
        {
            var soapFormatter = new SoapFormatter();

            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                soapFormatter.Serialize(file, groups);
            }

            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate))
            {
                if (soapFormatter.Deserialize(file) is List<Group> newGroups)
                {
                    foreach (var group in groups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
        }

        //< ------BinaryFormatter------ >

        public void BinaryFormatter(List<Group> groups, List<Student> students)
        {
            for (int i = 0; i < 10; i++)
            {
                var group = new Group(i, "Group: " + i);
                group.SetPrivate(i);
                groups.Add(group);
            }

            for (int i = 0; i < 300; i++)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100)
                {
                    Group = groups[i % 9]
                };

                students.Add(student);

            }
            var binFormater = new BinaryFormatter();

            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                binFormater.Serialize(file, groups);
            }

            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate))
            {
                if (binFormater.Deserialize(file) is List<Group> newGroups)
                {
                    foreach (var group in groups)
                    {
                        Console.WriteLine(group);
                    }
                }
            }
        }
    }

}
