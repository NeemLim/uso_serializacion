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
            WriteLine("Hello World!");
            var circleHolder = new circle();
            circleHolder.radium = 12;
            WriteLine($"{circleHolder.radium}");

            #region serialize
            var xs = new XmlSerializer(typeof(circle));
            string path = Combine(CurrentDirectory, "shapes.xml");
            using (FileStream stream = File.Create(path))
            {
                xs.Serialize(stream, circleHolder);
            }
            #endregion

            List<circle> circulos = new List<circle>();

            #region deserialize
            using (FileStream xmlLoad = File.Open(path, FileMode.Open))
            {
                // deserialize and cast the object graph into a List of Person 
                var loadedPeople = (circle)xs.Deserialize(xmlLoad);
                WriteLine($"{loadedPeople.radium}");
            }
            #endregion

        }
    }
}
