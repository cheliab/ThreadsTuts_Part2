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
            // GetThreadData();
            // CreateThread();
            // CreateParameterizedThread();
            
            Console.ReadLine();
        }
        
        #region ParameterizedThread
        
        private static void CreateParameterizedThread()
        {
            var number = 4;
            
            // Создаем поток
            Thread myThread = new Thread(new ParameterizedThreadStart(CountParam));
            myThread.Start(number);

            for (var i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * number);
                
                Thread.Sleep(300);
            }
        }

        private static void CountParam(object x)
        {
            int number = (int)x;
            
            for (var i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i + number);
                
                Thread.Sleep(400);
            }
        }
        
        #endregion
        
        #region ThreadStartDelegate

        /// <summary>
        /// Создание простого потока без параметров
        /// </summary>
        private static void CreateThread()
        {
            // Пример создания нового потока 
            // Thread newThread = new Thread(new ThreadStart(Count)); // конструктор с делегатом
            Thread newThread = new Thread(Count); // коструктор с неявным использования делегата
            
            newThread.Start(); // запускаем поток

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                
                Thread.Sleep(300);
            }
        }

        private static void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * i);
                
                Thread.Sleep(400);
            }    
        }
        
        #endregion
        
        /// <summary>
        /// Получение данных о потоке
        /// </summary>
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