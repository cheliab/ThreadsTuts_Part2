using System;
using System.Threading;

namespace ThreadSync_lock
{
    class Program
    {
        private static int x = 0;

        private static object locker = new object();
        
        static void Main(string[] args)
        {
            // StartThreads_WithoutLock();
            // StartThreads_WithLock();
            
            Monitor_WriteFile.Start();

            Console.ReadLine();
        }

        /// <summary>
        /// Пример потоков без использования синхронизации (lock)
        /// </summary>
        private static void StartThreads_WithoutLock()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(Count);
                thread.Name = $"Поток {i.ToString()}";
                thread.Start();
            }
        }
        
        private static void Count()
        {
            x = 1;

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Пример потоков c использованием синхронизации
        /// </summary>
        private static void StartThreads_WithLock()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(Count_WithLock);
                thread.Name = $"Поток {i.ToString()}";
                thread.Start();
            }
        }

        private static void Count_WithLock()
        {
            lock (locker)
            {
                x = 1;

                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                    x++;
                    Thread.Sleep(100);
                }
            }
        }
    }
}