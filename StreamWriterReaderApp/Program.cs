using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StreamWriterReaderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** Fun with StreamWriter / StreamReader ******\n");

            // Получить StreamWriter и записать строковые данные
            using(StreamWriter writer = File.CreateText("reminders.txt"))
            {
                writer.WriteLine("Don't forget Mother's day this year...");
                writer.WriteLine("Don't forget Father's day this year...");
                writer.WriteLine("Don't forget these numbers: ");

                for (int i = 0; i < 10; i++)
                {
                    writer.Write(i + " ");
                }

                // Вставить новую строку
                writer.Write(writer.NewLine);
            }
            Console.WriteLine("Created file and wrote some thoughts...");

            // Прочиать данные из файла
            Console.WriteLine("Here are your thoughts:\n");
            using(StreamReader reader = File.OpenText("reminders.txt"))
            {
                string input = null;
                while ((input = reader.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }
            Console.ReadLine();
        }
    }
}
