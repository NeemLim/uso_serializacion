using System;

namespace uso_serializacion
{
    [Serializable]
    public class Rectangle : Shape
    {
        public int lenght { get; set; }
        public int width { get; set; }
    }
}