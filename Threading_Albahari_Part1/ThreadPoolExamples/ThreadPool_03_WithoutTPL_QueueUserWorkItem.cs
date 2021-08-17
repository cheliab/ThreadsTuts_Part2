using System;
using System.Threading;

namespace Threading_Albahari_Part1.ThreadPoolExamples
{
    /// <summary>
    /// Использование пула потоков без библиотеки TPL (Task parallel library)
    /// </summary>
    public class ThreadPool_03_WithoutTPL_QueueUserWorkItem
    {
        public static void Start()
        {
            ThreadPool.QueueUserWorkItem(Go);
            ThreadPool.QueueUserWorkItem(Go, 123);
        }

        private static void Go(object data)
        {
            Console.WriteLine($"Hello from thread pool! {data}");
        }
    }
}