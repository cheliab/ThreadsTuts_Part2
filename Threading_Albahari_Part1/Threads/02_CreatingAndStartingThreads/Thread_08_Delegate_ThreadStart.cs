using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Варинты создания новых потоков
    ///
    /// Делегат, метод, лябда выражение
    /// </summary>
    public class Thread_08_Delegate_ThreadStart
    {
        /// <summary>
        /// Для запуска метода без параметров используется делегат ThreadStart 
        /// </summary>
        // public delegate void ThreadStart();
        
        public static void Start()
        {
            Thread thread_1 = new Thread(new ThreadStart(Go));
            Thread thread_2 = new Thread(Go);
            Thread thread_3 = new Thread(() => Console.WriteLine("hello!"));
            
            thread_1.Start();
            thread_2.Start();
            thread_3.Start();
            
            Go();
        }

        private static void Go()
        {
            Console.WriteLine("hello!");
        }
    }
}