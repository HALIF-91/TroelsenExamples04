using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleFileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Работа с типом FileInfo
            // Создать новый файл через FileInfo.Open()
            FileInfo f2 = new FileInfo("Test2.dat");
            using(FileStream fs2 = f2.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                // Использовать объект FileStream
            }

            // Получить объект FileStream с правами только для чтения
            FileInfo f4 = new FileInfo("Test3.dat");
            using(FileStream readOnlyStream = f4.OpenRead())
            {
                // Использовать объект FileStream
            }

            // Получить объект StreamReader
            FileInfo f5 = new FileInfo("boot.ini");
            using(StreamReader reader = f5.OpenText())
            {
                // Использовать объект StreamReader
            }

            // Получить объект StreamWriter
            FileInfo f6 = new FileInfo("Test6.txt");
            using(StreamWriter writer = f6.CreateText())
            {
                // Использовать объект StreamReader
            }
            
            FileInfo f7 = new FileInfo("FinalTest.txt");
            using(StreamWriter writer = f6.AppendText())
            {
                // Использовать объект StreamReader
            }
            #endregion

            #region Работа с типом File
            // Получить объект FileStream
            using (FileStream fs = File.Create("Test.dat")) { }

            using(FileStream fs2 = File.Open("Test2.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) { }

            // Получить объект FileStream с правами только для чтения
            using(FileStream readOnlyStream = File.OpenRead("Test.dat")) { }

            // Поучить объект FileStream с правами только для записи
            using(FileStream writeOnlyStream = File.OpenWrite("Test4.dat")) { }

            // Получить объект StreamReader
            using(StreamReader sreader = File.OpenText("boot.ini")) { }

            // Получить объект StreamWriter
            using(StreamWriter swriter = File.CreateText("Test6.txt")) { }

            using(StreamWriter swriterAppend = File.AppendText("FinalTest.txt")) { }

            Console.WriteLine("********* Simple I/O with the File Type ********");
            string[] myTasks =
            {
                "Fix bathroom sink", "Call Dave",
                "Call Mom and Dad", "Play Xbox 360"
            };

            // Записать все данные в файл
            File.WriteAllLines("tasks.txt", myTasks);

            // Прочитать все данные и вывести на консоль
            foreach (string task in File.ReadAllLines("tasks.txt"))
            {
                Console.WriteLine("TODO: {0}", task);
            }
            #endregion

            Console.ReadLine();
        }
    }
}
