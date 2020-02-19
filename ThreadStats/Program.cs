using System;
using System.Threading;

namespace ThreadStats
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******* Primary Thread stats *******");

            // Получить имя текущего потока
            Thread primaryThread = Thread.CurrentThread;

            primaryThread.Name = "ThePrimaryThread";

            // Показать детали включающего домена приложений и контекста
            Console.WriteLine("Name of current AppDomain: {0}", Thread.GetDomain().FriendlyName);

            Console.WriteLine("ID of current Context: {0}", Thread.CurrentContext.ContextID);

            // Вывести некоторую статистику о текущем потоке
            Console.WriteLine("Thread Name: {0}", primaryThread.Name); // имя потока

            Console.WriteLine("Has thread is started?: {0}", primaryThread.IsAlive); // запущен?

            Console.WriteLine("Priority level: {0}", primaryThread.Priority); // приоритет

            Console.WriteLine("Thread state: {0}", primaryThread.ThreadState); // состояние

            Console.ReadLine();
        }
    }
}
