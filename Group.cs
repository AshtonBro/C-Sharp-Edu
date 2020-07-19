using System;
using System.Collections.Generic;
using System.Text;

namespace AshtonBro.Code
{
    class Group
    {
        private Random rnd = new Random(DateTime.Now.Millisecond);
        public int Number { get; set; }
        public string Name { get; set; }
        public Group() 
        {
            Number = rnd.Next(1, 10);
            Name = "Group: " + rnd;
        }
        public Group(int number, string name)
        {
            // проверка входных параметров
            Number = number;
            Name = name;
        }
        public override string ToString()
        {
            return Number.ToString(); 
        }
    }
}
