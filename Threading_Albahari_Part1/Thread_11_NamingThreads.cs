using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    public class Thread_11_NamingThreads
    {
        public static void Start()
        {
            Thread.CurrentThread.Name = "Main";

            Thread worker = new Thread(Go);
            worker.Name = "Worker";
            worker.Start();
            
            Go();
        }

        private static void Go()
        {
            Console.WriteLine($"Hello from {Thread.CurrentThread.Name}");
        }
    }
}