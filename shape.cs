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
        public abstract void getParamaters();

        public double UserInput()
        {
            bool isValidDouble = false;
            double UserInput = 0;
            do
            { //Loop to get the desired input.
                //Checks for a double.
                Write("Type a valid double type value: ");
                isValidDouble = double.TryParse(
                    ReadLine(), out UserInput
                );

            } while (isValidDouble == false);

            return UserInput;
        }
    }

}