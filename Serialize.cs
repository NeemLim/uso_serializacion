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
            #region Variables
            string shapeListFilePath = Combine(CurrentDirectory, "ShapeList.xml");
            var XmlSerializer = new XmlSerializer(typeof(List<Shape>));
            char choice = 'x';
            bool correctInput = false;
            #endregion
            do
            {
                WriteLine("\n --- Main menu --- ");
                WriteLine("1 = Create or add shapes to ShapeList.xml");
                WriteLine("2 = Delete current file");
                WriteLine("3 = Calculate Area");
                WriteLine("0 = Finish program");
                Write(">Choice: ");
                choice = ReadKey().KeyChar;

                correctInput = choice switch
                {
                    //ASCII Value of Chars 1 to 3.
                    var x when (x >= 49 & x <= 52) => true,
                    _ => false,
                };
                WriteLine("");
            } while (correctInput == false);

            switch (choice)
            {
                case '1': //Add shapes.
                    #region SerializeVariables
                    var shapeList = new List<Shape>();
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
                    int shapeCount = shapeList.Count; 
                    createShape(ref userShape, shapeCount);
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

                case '2': //Delete current file
                    if (File.Exists(shapeListFilePath) == true)
                    {
                        File.Delete(shapeListFilePath);
                        WriteLine("Deletion succesful.");
                    }
                    else
                        WriteLine("No file to delete.");
                    break;

                case '3':
                    #region deserialize
                    using (FileStream xmlLoad = File.Open(shapeListFilePath, FileMode.Open))
                    {
                        // deserialize and cast the object graph into a List of Person 
                        var loadedPeople = (List<Shape>)XmlSerializer.Deserialize(xmlLoad);
                        foreach (Circle item in loadedPeople)
                        {
                            WriteLine(item.radium);
                        }
                    }
                    #endregion
                    break;

                case '0':
                    WriteLine("\nThank you, goodbye.");
                    return;
            }




        }

        static void createShape(ref Shape chosenShape, int itemCount)
        {
            char choice = 'x';
            do
            {
                WriteLine("\nShape Menu");
                WriteLine("C = Circle");
                WriteLine("R = Rectangle");
                WriteLine("T = Triangle");
                Write("Select your shape: ");
                choice = ReadKey().KeyChar;
                choice = char.ToUpper(choice);

                chosenShape = choice switch
                {
                    'C' => new Circle(),
                    'R' => new Rectangle(),
                    'T' => new Triangle(),
                    _ => null
                };
            } while (chosenShape == null);

            //Set ID for shape, first char + total item count.
            chosenShape.identifier = 
            char.ToString(choice) + itemCount.ToString();


            WriteLine("");
        }





    }

}
