using System;

namespace AshtonBro.Code
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)] // Используем атрибут AttributeTargets что присвоить к кокой структуре относить атрибут через точно можно получить большой список структур
    class GeoAttribute : System.Attribute
    {
        public int X { get; set; }
        public int Y { get; set; }

        public GeoAttribute() { }
        public GeoAttribute(int x, int y)
        {
            // проверка входных данных
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"[ОсьX: {X}; ОсьY: {Y};]";
        }
    }
}
