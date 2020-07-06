using System;
using System.Collections.Generic;
using System.Linq;

namespace AshtonBro.Code
{
    class Parkink
    {
        private List<Car> _cars = new List<Car>();
        private const int MAX_CARS = 100;
        public int Count => _cars.Count; // Доступ на чтение(быстрое свойство), позволяет обьявить публичное свойство которое предостваляет доступ к закрытому свойству с правами только на чтение
        public string Name { get; set; }
        public int Add(Car car)
        {
            if(car == null)
            {
                throw new ArgumentNullException(nameof(car), "Car is NULL");
            }

            if(_cars.Count < MAX_CARS)
            {
                _cars.Add(car);
                return _cars.Count - 1;
            }

            return -1;
        }

        public void GoOut(string number)
        {
            if(string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException(nameof(number), "Number is NULL or empty");
            }

            var car = _cars.FirstOrDefault(c => c.Name == number);
            if(car != null)
            {
                _cars.Remove(car);
            }
        }
    }
}
