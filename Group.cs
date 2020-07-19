using System;
using System.Collections.Generic;
using System.Text;

namespace AshtonBro.Code
{
    class Group
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Group() { }
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
