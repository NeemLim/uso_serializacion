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
            var XmlSerializer = new XmlSerializer(typeof(List<Shape>));
            #region Variables
            string shapeListFilePath = Combine(CurrentDirectory, "ShapeList.xml");
            char choice = 'x';
            bool correctInput = false;
            #endregion

            #region MainMenu
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
                    //ASCII Value of Chars 0 to 3.
                    var x when (x >= 48 & x <= 51) => true,
                    _ => false,
                };
                WriteLine("");
            } while (correctInput == false);
            #endregion

            switch (choice)
            {
                case '1': //Add shapes.
                    #region Variables
                    int shapesToGenerate = GetIntInput();
                    var shapeList = new List<Shape>();
                    #endregion

                    DeserializeLoadXML(shapeListFilePath, ref shapeList);

                    #region CreateShape
                    for (int N = 0; N < shapesToGenerate; N++)
                    {
                        Shape userShape = null;
                        int shapeCount = shapeList.Count;
                        createShape(ref userShape, shapeCount);
                        userShape.DefineAttributes();
                        shapeList.Add(userShape);
                    }
                    #endregion

                    SerializeList(shapeListFilePath, ref shapeList); 

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

                case '3': //Get shape area
                    #region AreaMenu
                    do
                    {
                        WriteLine("\n --- Area menu --- ");
                        WriteLine("1 = Area of all shapes");
                        WriteLine("2 = Area of a specific shape type");
                        WriteLine("3 = Area by shape index");
                        Write(">Choice: ");
                        choice = ReadKey().KeyChar;

                        correctInput = choice switch
                        {
                            //ASCII Value of Chars 1 to 3.
                            var x when (x >= 49 & x <= 51) => true,
                            _ => false,
                        };
                        WriteLine("");

                    } while (correctInput == false);
                    #endregion
                    List<Shape> loadedShapes = null;
                    DeserializeLoadXML(shapeListFilePath, ref loadedShapes); //Load current list
                    #region GetArea
                    switch (choice)
                    {
                        case '1': //Area of all shapes
                            ReadArea(ref loadedShapes);
                            break;

                        case '2': //Specific shape type
                            #region TriangleMenu
                            Type shapeType = SelectShapeType();
                            if (shapeType == typeof(Triangle))
                            {
                                do
                                {
                                    WriteLine("\n --- Triangle menu --- ");
                                    WriteLine("0 = Area of all triangles");
                                    WriteLine("1 = Area of equilateral triangles");
                                    WriteLine("2 = Area of isosceles triangles");
                                    WriteLine("3 = Area of scalene triangles");
                                    Write(">Choice: ");
                                    choice = ReadKey().KeyChar;
                                    correctInput = choice switch
                                    {
                                        //ASCII Value of Chars 0 to 3.
                                        var x when (x >= 48 & x <= 51) => true,
                                        _ => false,
                                    };
                                    WriteLine("");

                                } while (correctInput == false);
                                #endregion
                                if (choice == 48)
                                    ReadArea(ref loadedShapes, shapeType);
                                else
                                    ReadArea(ref loadedShapes, shapeType, choice);
                            }
                            else //Rectangle or circle type.
                                ReadArea(ref loadedShapes, shapeType);
                            break;

                        case '3': //Area by shape Index
                            string userShapeId = UserShapeId();
                            ReadArea(ref loadedShapes, userShapeId);
                            /* else
                                WriteLine($"Shape with ID = {userShapeId}  was not found"); */
                            break;
                    }
                    #endregion
                    
                    SerializeList(shapeListFilePath, ref loadedShapes); //Serialize area changes.

                    break;

                case '0':
                    WriteLine("\nThank you, goodbye.");
                    return;
            }
        }



        static void DeserializeLoadXML(string path, ref List<Shape> list)
        {
            var XmlSerializer = new XmlSerializer(typeof(List<Shape>));
            //Check if shapes.xml exists, deserialize and load it to current list of shapes.
            if (File.Exists(path) == true)
            {
                using (FileStream xmlLoad = File.Open(path, FileMode.Open))
                    list = (List<Shape>)XmlSerializer.Deserialize(xmlLoad);
            }
        }
        static void SerializeList(string path, ref List<Shape> list)
        {
            var XmlSerializer = new XmlSerializer(typeof(List<Shape>));
            using (FileStream stream = new FileStream(path, FileMode.Create))
                XmlSerializer.Serialize(stream, list);
        }

        static void createShape(ref Shape chosenShape, int itemCount)
        {
            char choice = 'x';
            WriteLine("\nShape Menu");
            WriteLine("C = Circle");
            WriteLine("R = Rectangle");
            WriteLine("T = Triangle");
            do
            {

                WriteLine("Select a valid shape.");
                Write("Shape choice: ");
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
            chosenShape.identifier = char.ToString(choice) + itemCount.ToString();
            WriteLine("");
        }

        static int GetIntInput()
        {
            bool isValidInt = false;
            int userInput = 0;
            WriteLine("Input the number of shapes to be created");
            do
            { //Loop to get the desired input.
              //Checks for an int.
                Write("\bBetween 1 and 1000: ");
                isValidInt = int.TryParse(
                    ReadLine(), out userInput
                );

                //Checks for integer to meet the constrainsts.
                if (userInput <= 1000 & userInput > 0)
                {
                    WriteLine($"Great! You will be asked for {userInput} shape(s)");
                }
                else
                {
                    //sets flag false to avoid the exiting loop.
                    isValidInt = false;
                    WriteLine("Nah, try again.");
                }

            } while (isValidInt == false);

            return userInput;
        }

        static void ReadArea(ref List<Shape> loadedShapes)
        {
            foreach (var item in loadedShapes)
                WriteLine($"Area of {item.identifier} is equal to {item.GetArea()} square units.");
        }

        static void ReadArea(ref List<Shape> loadedShapes, string shapeId)
        {
            foreach (var item in loadedShapes)
                if (item.identifier.Equals(shapeId))
                {
                WriteLine($"Area of {item.identifier} is equal to {item.GetArea()} square units.");
                break; 
                }
        }

        static void ReadArea(ref List<Shape> loadedShapes, Type shapeType)
        {
            foreach (var item in loadedShapes)
                if (item.GetType().Equals(shapeType)) //Matches with selected type.
                    WriteLine($"Area of {item.identifier} is equal to {item.GetArea()} square units.");

        }

        static void ReadArea(ref List<Shape> loadedShapes, Type shapeType, char choice)
        {
            int triangleType = choice - '0'; //Resta 48 y da el valor numerico.
            foreach (var item in loadedShapes)
                if (item.GetType().Equals(shapeType)) //Matches with selected type.
                {
                    Triangle currentTriangle = item as Triangle;
                    if (currentTriangle.Type == triangleType)
                        WriteLine($"Area of {item.identifier} is equal to {item.GetArea()} square units.");
                }
        }

        static Type SelectShapeType()
        {
            Type shapeType;
            char choice = 'x';
            WriteLine("--- Shape Menu --- ");
            WriteLine("C = Circles");
            WriteLine("R = Rectangles");
            WriteLine("T = Triangles");
            do
            {
                WriteLine("Select a shape type.");
                Write("Shape choice: ");
                choice = char.ToUpper(ReadKey().KeyChar);

                shapeType = choice switch
                {
                    'C' => typeof(Circle),
                    'R' => typeof(Rectangle),
                    'T' => typeof(Triangle),
                    _ => null,
                };

            } while (shapeType == null);

            WriteLine("");

            return shapeType;

        }

        static string UserShapeId()
        {
            WriteLine("Input the ID of the shape: ");
            string shapeId = ReadLine();
            return shapeId;
        }

    }

}
