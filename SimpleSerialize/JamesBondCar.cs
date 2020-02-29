using System;
using System.Xml.Serialization;

namespace SimpleSerialize
{
    // XmlRoot - атрибуты из пространства Xml.Serialization
    // [Serializable]
    [Serializable, XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class JamesBondCar : Car
    {
        [XmlAttribute]              // атрибуты из пространства Xml.Serialization
        public bool canFly;
        [XmlAttribute]              // атрибуты из пространства Xml.Serialization
        public bool canSubmerge;

        public JamesBondCar(bool skyWorthy, bool seaWorthy)
        {
            canFly = skyWorthy;
            canSubmerge = seaWorthy;
        }

        // XmlSerializer требует стандартного конструктора!
        public JamesBondCar() { }
    }
}
