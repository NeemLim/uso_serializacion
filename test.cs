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
            string path = Combine(CurrentDirectory, "shapes.xml");
            var xs = new XmlSerializer(typeof(List<Shape>));
            
            var figuras = new List<Shape>();
            WriteLine("Hello World!");
            var circleHolder = new Circle();
            circleHolder.radium = 13;
            WriteLine($"{circleHolder.radium}");
            
          
             using (FileStream xmlLoad = File.Open(path, FileMode.Open))
            {
                // deserialize and cast the object graph into a List of Person 
                figuras = (List<Shape>)xs.Deserialize(xmlLoad);
            } 

            figuras.Add(circleHolder); 


            #region serialize
            //using (FileStream stream = File.Create(path)) //crear archivo
            using (FileStream stream = new FileStream(path, FileMode.Create)) //añadir al archivo
            {
                xs.Serialize(stream, figuras);
            }
            #endregion
          

            #region deserialize
            using (FileStream xmlLoad = File.Open(path, FileMode.Open))
            {
                // deserialize and cast the object graph into a List of Person 
                var loadedPeople = (List<Shape>)xs.Deserialize(xmlLoad);
                foreach (Circle item in loadedPeople)
                {
                    WriteLine(item.radium);
                }
            }
            #endregion

        }
    }
}
