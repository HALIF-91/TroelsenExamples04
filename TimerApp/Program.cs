using System;
using System.Threading;

namespace TimerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******* Working with Timer type *******\n");
            Console.WriteLine("Id of thread: {0}", Thread.CurrentThread.ManagedThreadId);
            // Создать делегат для типа Timer
            TimerCallback timeCB = new TimerCallback(PrintTime);

            // Установить параметры таймера
            Timer t = new Timer(
                timeCB,             // Объект делагата TimerCallback
                "Hello Buddy",      // Информация для передачи в вызванный метод
                0,                  // Период времени ожидания перед запуском (в миллисекундах)
                1000);              // Интервал времени между вызовами (в миллисекундах)
            Console.WriteLine("Hit key to terminate...");
            Console.ReadLine();
        }

        private static void PrintTime(object state)
        {
            Console.WriteLine("Id of thread: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Time is: {0}, Param is: {1}", DateTime.Now.ToLongTimeString(), state.ToString());
        }
    }
}
