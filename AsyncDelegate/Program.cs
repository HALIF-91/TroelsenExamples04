using System;
using System.Threading;

namespace AsyncDelegate
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******* Async Delegate Invocation ******");

            // Вывести идентификатор выполняющегося потока
            Console.WriteLine("Main() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            // Вызвать Add() во вторичном потоке
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10, null, null);

            // Выполнить другую работу в первичном потоке
            Console.WriteLine("Doing more work in Main()!");

            // Это сообщение продолжит выводиться до тех пор, пока не будет завершен метод Add()
            while (!iftAR.IsCompleted)
            {
                Console.WriteLine("Doing more work in Mainnnnnnnnnnn()!");
                Thread.Sleep(1000);
            }

            // Можно задавать максимальное время ожидания, если это время истекает
            // метод WaitOne() возвращает false
            /*while (!iftAR.AsyncWaitHandle.WaitOne(2000, false))
            {
                Console.WriteLine("Doing more work in Main()!");
            }*/

            // Теперь известно, что метод Add() завершен, получить результат по готовности
            int answer = b.EndInvoke(iftAR);
            Console.WriteLine("10 + 10 is {0}", answer);
            Console.ReadLine();
        }

        private static int Add(int x, int y)
        {
            // Вывести идентификатор выполняющегося потока
            Console.WriteLine("Add() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            // Организовать паузу для моделирования длительной операции
            Thread.Sleep(5000);
            return x + y;
        }
    }
}
