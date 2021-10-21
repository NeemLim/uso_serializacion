
using static System.Console;
using static System.Math; 

namespace uso_serializacion
{
    public class Circle : Shape
    {
        public double radius { get; set; }
        public override void DefineAttributes()
        {
            WriteLine("Input the radium of the circle.");
            radius = UserInput();
        }
        public override double GetArea()
        {
            //A = π * r^2
            area = radius * radius * PI; 
            area = Round(area, 2); 
            return area; 
        }

    }
}