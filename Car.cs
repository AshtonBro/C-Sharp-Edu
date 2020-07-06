using System;
namespace AshtonBro.Code
{
    class Car
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"Авто: {Name}, Номер: {Number}";
        }
    }
}
