using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static readonly object _object = new object();
        static Thread mainThread, thread1, thread2, thread;

        static void Main(string[] args)
        {
            mainThread = Thread.CurrentThread;
            thread1 = new Thread(ThreadProc);
            thread1.Name = "Thread1";
            thread1.Start();


            thread2 = new Thread(ThreadProc);
            thread2.Name = "Thread2";
            thread2.Start();

            // Create ten new threads.
            for (int i = 0; i < 10; i++)
            {
                ThreadStart start = new ThreadStart(A);
                new Thread(start).Start();

            }

            Console.ReadKey();

        }

        private static void ThreadProc()
        {
            Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
            if (Thread.CurrentThread.Name == "Thread1" &&
                thread2.ThreadState != ThreadState.Unstarted)
                thread2.Join();

            Thread.Sleep(2000);
            Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
            Console.WriteLine("Thread1: {0}", thread1.ThreadState);
            Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);
        }

        static void A()
        {
            // Lock on the readonly object.
            // ... Inside the lock, sleep for 300 milliseconds.
            // ... This is thread serialization.
            lock (_object)
            {
                Thread.Sleep(300);
                Console.WriteLine(Environment.TickCount);
            }
        }
    }
}
