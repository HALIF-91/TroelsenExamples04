using System;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace MultiThreadedPrinting
{
    //[Synchronization]
    class Printer// : ContextBoundObject
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

        #region Синхронизация с использованием типа System.Threading.Monitor
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
        #endregion

        #region Синхронизация с использованием типа System.Threading.Interlocked
        private int intVal;
        public void AddOne()
        {
            // не только изменяет значение входного параметра, но также возвращает
            int newVal = Interlocked.Increment(ref intVal);
        }
        public void SafeAssignment()
        {
            // присвоить значение 83 переменной-члену
            Interlocked.Exchange(ref intVal, 83);
        }
        public void CompareAndExchange()
        {
            // Если значение i равно 83, заменить его 99
            Interlocked.CompareExchange(ref intVal, 99, 83);
        }
        #endregion
    }
}
