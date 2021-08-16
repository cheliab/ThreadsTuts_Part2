using System;
using System.Threading.Tasks;

namespace Threading_Albahari_Part1.ThreadPool
{
    public class ThreadPool_01_EnteringTheThreadPool
    {
        public static void Start()
        {
            Task.Factory.StartNew(Go);
        }

        private static void Go()
        {
            Console.WriteLine("Hello from the thread pool!");
        }
    }
}