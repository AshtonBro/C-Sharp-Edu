using System;

namespace AshtonBro.Code
{   
    
    public class Photo
    {
        [Geo(14, 35)]
        public string Name { get; set; }
        public string Path { get; set; }
        public Photo(string name)
        {
            // Проверка входных параметров
            Name = name;
        }
    }
}
