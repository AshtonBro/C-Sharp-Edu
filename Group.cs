using System;
using System.Collections.Generic;
using System.Text;

namespace AshtonBro.Code
{
    [Serializable]
    class Group
    {
        [NonSerialized]
        private Random rnd = new Random(DateTime.Now.Millisecond);
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
}
