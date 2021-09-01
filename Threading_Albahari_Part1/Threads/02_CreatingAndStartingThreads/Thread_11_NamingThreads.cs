using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// У каждого потока есть свойство Name, которое можно установить для отладки.
    /// </summary>
    public class Thread_11_NamingThreads
    {
        public static void Start()
        {
            // Статическое Thread.CurrentThread свойство дает вам текущий выполняющийся поток.
            Thread.CurrentThread.Name = "Main"; // Тут устанавливается имя основного потока

            Thread worker = new Thread(Go);
            worker.Name = "Worker"; // тут имя для вспомогательного потока
            worker.Start();
            
            Go();
        }

        private static void Go()
        {
            Console.WriteLine($"Hello from {Thread.CurrentThread.Name}");
        }
    }
}