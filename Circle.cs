
using System;
using static System.Console;
using static System.Math; 

namespace uso_serializacion
{
    public class Circle : Shape
    {
        public double radius { get; set; }
        public override void GetParamaters()
        {
            WriteLine("Input the radium of the circle.");
            radius = UserInput();
        }
        public override void GetArea()
        {
            //A = π * r^2
            area = radius * radius * PI; 
            Round(area, 2); 
        }

    }
}