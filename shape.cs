using System;
using System.Xml.Serialization; 


namespace uso_serializacion
{
    [XmlInclude(typeof(Circle))]
    [Serializable]
    public class Shape
    {
        
    }
}