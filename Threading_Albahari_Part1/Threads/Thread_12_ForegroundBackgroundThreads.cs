using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    public class Thread_12_ForegroundBackgroundThreads
    {
        public static void Start()
        {
            Thread worker = new Thread(() => Thread.Sleep(5000));
            
            // По умолчанию создаваемые потоки являются потоками переднего плана
            Console.WriteLine($"IsBackground = {worker.IsBackground}");

            // Если закоменировать то приложение не завершится пока не выполнится созданный поток
            // Если оставить по приложение завершиться сразу
            // worker.IsBackground = true;
            
            // Приложение не будет ждать завершения фонового потока
            Console.WriteLine($"IsBackground = {worker.IsBackground}");
            
            worker.Start();
            
            Thread.Sleep(1000);
        }
    }
}