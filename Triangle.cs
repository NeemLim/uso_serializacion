using static System.Console;
namespace uso_serializacion
{
    public class Triangle : Shape
    {
        public override void getParamaters()
        {
            WriteLine("Input the side A of the triangle.");
            sideA = UserInput();
            WriteLine("Input the side B of the triangle.");
            sideB = UserInput();
            WriteLine("Input the side C of the triangle.");
            sideC = UserInput();
        }
        public double sideA { get; set; }
        public double sideB { get; set; }
        public double sideC { get; set; }
    }
}