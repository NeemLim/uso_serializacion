using static System.Console;
using static System.Math; 

namespace uso_serializacion
{
    public class Rectangle : Shape
    {
        public double lenght { get; set; }
        public double width { get; set; }
        public override void GetParamaters()
        {
            WriteLine("Input the lenght of the rectangle.");
            lenght = UserInput();
            WriteLine("Input the width of the rectangle.");
            width = UserInput();
        }
        public override double GetArea()
        {
            //A = π * r^2
            area = lenght * width; 
            area = Round(area, 2); 
            return area; 
        }


    }
}