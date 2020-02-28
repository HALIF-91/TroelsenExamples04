using System;
using System.Text;
using System.IO;

namespace FileStreamApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** Fun with FileStreams ********");

            // Получить оьъект FileStream
            using (FileStream stream = File.Open("myMessage.dat", FileMode.Create))
            {
                // Закодировать строку в виде массива байт
                string msg = "Hello!";
                byte[] msgAsByteArray = Encoding.Default.GetBytes(msg);

                // Записать byte[] в файл
                stream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

                // Сбросить внутреннюю позицию потока
                stream.Position = 0;

                // Прочитать типы из файла и вывести на консоль
                Console.Write("Your message as an array of bytes: ");
                byte[] bytesFromFile = new byte[msgAsByteArray.Length];

                for (int i = 0; i < msgAsByteArray.Length; i++)
                {
                    bytesFromFile[i] = (byte)stream.ReadByte();
                    Console.Write(bytesFromFile[i]);
                }

                // Вывести декодированные сообщения
                Console.Write("\nDecoded Message: ");
                Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
            }
            Console.ReadLine();
        }
    }
}
