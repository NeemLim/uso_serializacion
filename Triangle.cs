using static System.Console;
using static System.Math;
namespace uso_serializacion
{

    public class Triangle : Shape
    {
        public double sideA { get; set; }
        public double sideB { get; set; }
        public double sideC { get; set; }

        public int Type { get; set; }
        enum triangleType
        {
            equilateral = 1,
            isosceles,
            scalene
        }
        public override void DefineAttributes()
        {
            WriteLine("Input the side A of the triangle.");
            sideA = UserInput();
            WriteLine("Input the side B of the triangle.");
            sideB = UserInput();
            WriteLine("Input the side C of the triangle.");
            sideC = UserInput();

            DefineTriangleType();
        }

        public void DefineTriangleType()
        {
            if (sideA == sideB && sideB == sideC)
            {
                Type = (int)triangleType.equilateral;
            }
            else if (sideA == sideB || sideA == sideC || sideB == sideC)
            {
                Type = (int)triangleType.isosceles;
            }
            else
            {
                Type = (int)triangleType.scalene;
            }
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