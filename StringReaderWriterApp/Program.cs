using System;
using System.IO;
using System.Text;

namespace StringReaderWriterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with StringWriter / StringReader ******\n");
            // Создать StringWriter и записать симвоьные данные в память
            using(StringWriter strWriter = new StringWriter())
            {
                strWriter.WriteLine("Don't forget Mother's Day this year...");
                // Получить копию содержимого (хранящегося в строке) и вывести на консоль
                Console.WriteLine("Contents of StringWriter:\n{0}", strWriter);

                // Получить внутренний StringBuilder
                StringBuilder sb = strWriter.GetStringBuilder();
                sb.Insert(0, "Hey!! ");
                Console.WriteLine("-> {0}", sb.ToString());
                sb.Remove(0, "Hey!! ".Length);
                Console.WriteLine("-> {0}", sb.ToString());

                // Читать данные из StringWriter
                using(StringReader strReader = new StringReader(strWriter.ToString()))
                {
                    string input = null;
                    while((input = strReader.ReadLine()) != null)
                    {
                        Console.WriteLine(input);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
