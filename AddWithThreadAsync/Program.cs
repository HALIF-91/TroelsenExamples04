using System;
using System.Threading.Tasks;
using System.Threading;

namespace AddWithThreadAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Add();
            Console.ReadLine();
        }

        private async static void Add()
        {
            Console.WriteLine("********** Adding with Thread objects *******");
            Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 10);
            await SumAsync(ap);

            Console.WriteLine("Other thread is done!");
        }
        static Task SumAsync(object data)
        {
            return Task.Run(()=>
            {
                if(data is AddParams)
                {
                    Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

                    AddParams ap = (AddParams)data;
                    Console.WriteLine("{0} + {1} is {2}", ap.a, ap.b, ap.a + ap.b);
                }
            });
        }
    }
}
