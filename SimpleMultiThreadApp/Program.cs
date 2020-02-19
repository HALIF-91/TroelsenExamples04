using System;
using System.Threading;
using System.Windows.Forms;

namespace SimpleMultiThreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** The Amazing Thread App **********\n");
            Console.Write("Do you want [1] or [2] threads?");
            string threadCount = Console.ReadLine();

            // Назначить имя текущему потоку
            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            // Вывести информацию о потоке
            Console.WriteLine("-> {0} is executing Main()", Thread.CurrentThread.Name);

            // Создать рабочий класс
            Printer p = new Printer();
            switch (threadCount)
            {
                case "1": p.PrintNumbers(); 
                    break;
                case "2": Thread backgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
                    backgroundThread.Name = "Secondary";
                    // Теперь это фоновый поток
                    backgroundThread.IsBackground = true;
                    backgroundThread.Start();
                    break;
                default:
                    Console.WriteLine("I don't know what you want...you get 1 thread");
                    goto case "1"; // для всех остальных вариантов принимается один поток
            }

            // выполнить некоторую дополнительную работу
            MessageBox.Show("I'm busy!", "Work on main thread...");
        }
    }
}
