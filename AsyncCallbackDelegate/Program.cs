using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace AsyncCallbackDelegate
{
    public delegate int BinaryOp(int x, int y);
    class Program
    {
        private static bool isDone = false;
        static void Main(string[] args)
        {
            Console.WriteLine("****** AsyncCallbackDelegate Example ********");
            Console.WriteLine("Main invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult itfAR = b.BeginInvoke(10, 10, new AsyncCallback(AddComplete), "Main() thanks you for adding these numbers");

            // Предположим что здесь выполнятеся какая-то другая работа
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working...");
            }
            Console.ReadLine();
        }

        private static void AddComplete(IAsyncResult itfAR)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is  complete");

            // Теперь получить результат
            AsyncResult ar = (AsyncResult)itfAR;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;
            Console.WriteLine("10 + 10 is {0}", b.EndInvoke(itfAR));

            // Получить информационный объект и привести его к string
            string msg = (string)itfAR.AsyncState;
            Console.WriteLine(msg);

            // завршение вторичного потока
            isDone = true;
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
