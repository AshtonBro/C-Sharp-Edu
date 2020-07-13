using System;

namespace AshtonBro.Code
{   
    [Geo(10, 20)]
    public class Photo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Photo(string name)
        {
            // Проверка
            Name = name;
        }
    }
}
