using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace CustomSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** Fun with Custom Serialization ********\n");

            #region Сериализация с использованием ISerializable
            // Этот тип реализует ISerializable
            StringData myData = new StringData("First data block", "More data");

            // Сохранить локальный файл в формате SOAP
            SoapFormatter soapFormat = new SoapFormatter();
            using (Stream stream = new FileStream("MyData.soap", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(stream, myData);
            }

            using(Stream stream = File.OpenRead("MyData.soap"))
            {
                StringData newData = (StringData)soapFormat.Deserialize(stream);

                Console.WriteLine($"{newData.dataItemOne} {newData.dataItemTwo}\n");
            }
            #endregion

            #region Сериализация с использованием атрибутов
            MoreData moreData = new MoreData("First data block", "More data");

            using (Stream stream = new FileStream("MoreData.soap", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(stream, moreData);
            }

            using (Stream stream = File.OpenRead("MoreData.soap"))
            {
                MoreData newMore = (MoreData)soapFormat.Deserialize(stream);

                Console.WriteLine($"{newMore.dataItemOne} {newMore.dataItemTwo}");
            }
            #endregion
            Console.ReadLine();
        }
    }
}
