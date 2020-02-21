using System;
using System.Threading;

namespace ThreadPoolApp
{
    public class Printer
    {
        public void PrintNumbers()
        {
            // Вывести информацию о потоке
            Console.WriteLine("-> {0} is executing PrintNumbers()", Thread.CurrentThread.ManagedThreadId);

            // Вывести числа
            Console.Write("Your numbers: ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i}, ");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }
    }
}
