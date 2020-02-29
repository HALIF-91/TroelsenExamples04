using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace SimpleSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** Fun with Object Serialization *********\n");
            // Создать JamesBondCar и установить состояние
            JamesBondCar jbc = new JamesBondCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] { 89.3, 105.1, 97.1 };
            jbc.theRadio.hasTweeters = true;

            // Сохранить объект в указанном файле в двоичном формате
            SaveAsBinaryFormat(jbc, "CarData.dat");
            LoadFromBinaryFile("CarData.dat");

            SaveAsSoapFormat(jbc, "CarData.soap");
            LoadFromSoapFile("CarData.soap");

            SaveAsXmlFormat(jbc, "CarData.xml");
            LoadFromXmlFile("CarData.xml");

            SaveListOfCars();
            Console.ReadLine();
        }

        #region BinaryFormatter
        static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            // Сохранить объект в файл CarData.dat в двоичном виде
            BinaryFormatter binFormat = new BinaryFormatter();

            using(Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(stream, objGraph);
            }
            Console.WriteLine("=> Saved car in binary format!");
        }
        static void LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            // Получить JamesBondCar из двоичного файла
            using(Stream stream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk = (JamesBondCar)binFormat.Deserialize(stream);
                Console.WriteLine("Can this Car fly?: {0}", carFromDisk.canFly);
            }
        }
        #endregion

        #region SoapFormatter
        static void SaveAsSoapFormat(object objGraph, string fileName)
        {
            // Сохранить объект в файле CarData.soap в формате SOAP
            SoapFormatter soapFormat = new SoapFormatter();

            using(Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(stream, objGraph);
            }
            Console.WriteLine("=> Saved car in SOAP format!");
        }
        static void LoadFromSoapFile(string fileName)
        {
            SoapFormatter soapFormat = new SoapFormatter();

            // Получить JamesBondCar из soap(xml) файла
            using (Stream stream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk = (JamesBondCar)soapFormat.Deserialize(stream);
                Console.WriteLine("Can this Car fly?: {0}", carFromDisk.canFly);
            }
        }
        #endregion

        #region XmlSerializer
        static void SaveAsXmlFormat(object objGraph, string fileName)
        {
            // Сохранить объект в файле CarData.xml в формате XML
            // XmlSerializer требует, чтобы все сериализированные типы
            // поддерживали стандартный конструктор
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar), new Type[] { typeof(Radio), typeof(Car) });

            using(Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(stream, objGraph);
            }
            Console.WriteLine("=> Saved car in XML format!");
        }
        static void LoadFromXmlFile(string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar), new Type[] { typeof(Radio), typeof(Car) });

            // Получить JamesBondCar из xml файла
            using (Stream stream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk = (JamesBondCar)xmlFormat.Deserialize(stream);
                Console.WriteLine("Can this Car fly?: {0}", carFromDisk.canFly);
            }
        }
        #endregion

        static void SaveListOfCars()
        {
            // Сохранить список List<T> объектов JamesBondCar
            List<JamesBondCar> myCars = new List<JamesBondCar>();
            myCars.Add(new JamesBondCar(true, true));
            myCars.Add(new JamesBondCar(true, false));
            myCars.Add(new JamesBondCar(false, true));
            myCars.Add(new JamesBondCar(false, false));

            using(Stream stream = new FileStream("CarCollection.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<JamesBondCar>));
                xmlFormat.Serialize(stream, myCars);
            }
            Console.WriteLine("=> Saved list of cars!");
        }
    }
}