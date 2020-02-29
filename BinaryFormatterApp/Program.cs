using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryFormatterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UserPrefs userData = new UserPrefs();
            userData.WindowColor = "Yellow";
            userData.FontSize = 50;

            // BinaryFormatter сохраняет данные в двоичном формате
            BinaryFormatter binFormat = new BinaryFormatter();

            // Сохранить объект в локальном файле
            using(Stream stream = new FileStream("user.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(stream, userData);
            }
            Console.ReadLine();
        }
    }
}
