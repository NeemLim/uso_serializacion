using static System.Console;
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
        public override void GetArea()
        {
            throw new System.NotImplementedException();
        }


    }
}