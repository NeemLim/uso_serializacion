using static System.Console;
using static System.Math; 
namespace uso_serializacion
{
    public class Triangle : Shape
    {
        public double sideA { get; set; }
        public double sideB { get; set; }
        public double sideC { get; set; }
        public override void GetParamaters()
        {
            WriteLine("Input the side A of the triangle.");
            sideA = UserInput();
            WriteLine("Input the side B of the triangle.");
            sideB = UserInput();
            WriteLine("Input the side C of the triangle.");
            sideC = UserInput();
        }
        public override double GetArea()
        {
                double semiP = (sideA + sideB + sideC) / 2;
                area = Sqrt(semiP * (semiP - sideA) * (semiP - sideA) * (semiP - sideA)); 
                area = Round(area, 2); 
                return area; 
        }

    }
}