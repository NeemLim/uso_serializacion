using System;
using System.Xml.Serialization;
using static System.Console;


namespace uso_serializacion
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Triangle))]
    [Serializable]
    public abstract class Shape
    {
        public string identifier { get; set; }
        public double area { get; set; }
        public abstract void DefineAttributes();
        public abstract double GetArea();

        public double UserInput()
        {
            bool isValidDouble = false;
            double userInput = 0;
            do
            { //Loop to get the desired input.
                //Checks for a double.
                Write("Type a positive double type value: ");
                isValidDouble = double.TryParse(
                    ReadLine(), out userInput
                );

                if (userInput < 0)
                    isValidDouble = false;  //sets flag false to avoid the exiting loop.

            } while (isValidDouble == false);

            return userInput;
        }
    }

}