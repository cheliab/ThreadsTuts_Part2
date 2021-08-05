using System;
using System.Threading;

namespace Threads_TutPart2_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start_UseManyThreads();
            // Start_UseThreadPool();
            GetThreadData();
            
            Console.ReadLine();
        }
        
        private static void GetThreadData()
        {
            // Получаем текущий поток
            Thread thread = Thread.CurrentThread;
            
            // Получить имя потока
            Console.WriteLine($"Имя потока: {thread.Name}");
            thread.Name = "Main";
            Console.WriteLine($"Имя потока: {thread.Name}");
            
            Console.WriteLine($"Запущен ли поток: {thread.IsAlive}");
            Console.WriteLine($"Приоритет потока: {thread.Priority}");
            Console.WriteLine($"Статус потока: {thread.ThreadState}");
            
            // Получаем домен приложения
            Console.WriteLine($"Домен приложения: {Thread.GetDomain().FriendlyName}");
        }
        
        /// <summary>
        /// Вариант цикла с 10 потоками
        /// </summary>
        private static void Start_UseManyThreads()
        {
            for (var i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(Work);
                thread.Start(i);
                
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Вариант с использованием пула потоков
        /// </summary>
        private static void Start_UseThreadPool()
        {
            for (var i = 1; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Work, i);
                
                Thread.Sleep(200);
            }
        }

        private static void Work(object i)
        {
            Console.WriteLine("Идентификатор потока: {0}, параметр: {1}", Thread.CurrentThread.ManagedThreadId, i);
        }
    }
}