using System;
using System.Diagnostics;
using System.Threading;

// Пример из статьи
// https://www.codeproject.com/Articles/28785/Thread-synchronization-Wait-and-Pulse-demystified

namespace ThreadSynq_Monitor_BlockingQueue
{
    class Program
    {
        private static BlockingQueue<int> _queue;
        
        static void Main(string[] args)
        {
            _queue = new BlockingQueue<int>(4);
            
            // Создаем поток добавляющий в очередь
            new Thread(Producer).Start();

            // Создаем два потока забирающих из очереди
            for (int i = 0; i < 2; i++)
            {
                new Thread(Consumer).Start();
            }
            
            Thread.Sleep(500);
            
            Console.WriteLine("Quitting");
            
            _queue.Quit();
            
            Console.ReadLine();
        }

        private static void Producer()
        {
            for (int x = 0;; x++) // постоянно добавляем элементы в очередь
            {
                // Добавляем элемент в очередь, если можно
                if (!_queue.Enqueue(x))
                    break;
                
                Console.WriteLine($"{x:0000} >");
            }
            Console.WriteLine("Producer quitting");
        }

        private static void Consumer()
        {
            for (;;) // while(true)
            {
                Thread.Sleep(100);

                // Получаем элемент из очереди, если можно
                int x = 0;
                if (!_queue.Dequeue(out x))
                    break;

                Console.WriteLine($"     < {x:0000}");
            }
            Console.WriteLine("Consumer quitting");
        }
    }
}