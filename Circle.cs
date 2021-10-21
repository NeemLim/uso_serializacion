using static System.Console;

namespace uso_serializacion
{
    public class Circle : Shape
    {
        public double radium { get; set; }
        public override void getParamaters()
        {
            WriteLine("Input the radium of the circle.");
            radium = UserInput(); 
        }

    }
}