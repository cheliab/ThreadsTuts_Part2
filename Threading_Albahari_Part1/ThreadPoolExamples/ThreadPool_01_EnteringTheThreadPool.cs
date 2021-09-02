using System;
using System.Threading.Tasks;

namespace Threading_Albahari_Part1.ThreadPoolExamples
{
    /// <summary>
    /// Вход в пул потоков используюя TPL (Task Parallel Library)
    /// </summary>
    public class ThreadPool_01_EnteringTheThreadPool
    {
        public static void Start()
        {
            // Чтобы использовать неуниверсальный Task класс, вызовите Task.Factory.StartNew, передав делегат целевого метода:
            
            // Запуск метода в потоке из пула потоков 
            Task.Factory.StartNew(Go);
            
            // Task.Factory.StartNew возвращает Task объект,
            // который затем можно использовать для отслеживания задачи - например,
            // вы можете дождаться ее завершения, вызвав его Wait() метод.
        }

        private static void Go()
        {
            Console.WriteLine("Hello from the thread pool!");
        }
    }
}