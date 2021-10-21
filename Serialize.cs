using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using static System.Console;

namespace uso_serializacion
{
    class Program
    {
        static void Main(string[] args)
        {
            char choice = '0';
            bool correctInput = false;
            do
            {
                WriteLine("\n --- Main menu --- ");
                WriteLine("1 = Create or add shapes to ShapeList.xml");
                WriteLine("2 = Delete current file");
                WriteLine("3 = Calculate Area");
                Write("Select your shape: ");
                choice = ReadKey().KeyChar;

                correctInput = choice switch
                {
                    //ASCII Value of Chars 1 to 3.
                    var x when (x >= 49 & x <= 51) => true,
                    _ => false,
                };
            } while (correctInput == false);

            switch (choice)
            {
                case '1':
                    #region Variables
                    string shapeListFilePath = Combine(CurrentDirectory, "ShapeList.xml");
                    var shapeList = new List<Shape>();
                    var XmlSerializer = new XmlSerializer(typeof(List<Shape>));
                    #endregion

                    #region LoadXML
                    //Check if shapes.xml exists, deserialize and load it to current list of shapes.
                    if (File.Exists(shapeListFilePath) == true)
                    {
                        using (FileStream xmlLoad = File.Open(shapeListFilePath, FileMode.Open))
                            shapeList = (List<Shape>)XmlSerializer.Deserialize(xmlLoad);
                    }
                    #endregion

                    #region AddShape
                    Shape userShape = null;
                    getShape(ref userShape);
                    userShape.getParamaters();
                    shapeList.Add(userShape);
                    #endregion

                    #region Serialize
                    using (FileStream stream = new FileStream(shapeListFilePath, FileMode.Create))
                    {
                        XmlSerializer.Serialize(stream, shapeList);
                    }
                    #endregion

                    break;
            }




        }

        static void getShape(ref Shape chosenShape)
        {
            do
            {
                WriteLine("\nShape Menu");
                WriteLine("C = Circle");
                WriteLine("R = Rectangle");
                WriteLine("T = Triangle");
                Write("Select your shape: ");
                char choice = ReadKey().KeyChar;

                choice = char.ToUpper(choice);
                chosenShape = choice switch
                {
                    'C' => new Circle(),
                    'R' => new Rectangle(),
                    'T' => new Triangle(),
                    _ => null
                };
            } while (chosenShape == null);
            WriteLine("");
        }




    }

}
