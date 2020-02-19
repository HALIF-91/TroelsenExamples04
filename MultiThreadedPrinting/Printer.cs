using System;
using System.Threading;

namespace MultiThreadedPrinting
{
    class Printer
    {
        // Маркер блокировки
        private object threadLock = new object();
        public void PrintNumbers()
        {
            lock (threadLock)
            {
                // Вывести информацию о потоке
                Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name);

                // Вывести числа
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    // Приостановить поток на случайный период времени
                    Random r = new Random();
                    Thread.Sleep(1000 * r.Next(5));
                    Console.Write($"{i}, ");
                }
                Console.WriteLine();
            }
        }
        public void ShowNumbers()
        {
            Monitor.Enter(threadLock);
            try
            {
                // Вывести информацию о потоке
                Console.WriteLine("-> {0} is executing ShowNumbers()", Thread.CurrentThread.Name);

                // Вывести числа
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    // Приостановить поток на случайный период времени
                    Random r = new Random();
                    Thread.Sleep(1000 * r.Next(5));
                    Console.Write($"{i}, ");
                }
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(threadLock);
            }
        }
    }
}
