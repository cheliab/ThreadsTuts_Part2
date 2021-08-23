using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    public class Locking_10_Semaphore
    {
        /// <summary>
        /// Облегченная версия обычного семафора
        /// </summary>
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3); // Емкость на три потока

        public static void Start()
        {
            for (int i = 1; i <= 5; i++) // Всего запускаем 5 потоков, т.е. 2 будут ожидать
            {
                new Thread(Enter).Start(i);
            }
        }

        private static void Enter(object id)
        {
            Console.WriteLine($"\"{id}\" - want to enter");
            
            _semaphore.Wait(); // В этом блоке могут быть всего 3 потока одновременно 
            
            Console.WriteLine($"\"{id}\" is in!");
            Thread.Sleep(1000 * (int)id);
            Console.WriteLine($"\"{id}\" is leaving");
            
            _semaphore.Release(); // Выход из блока
        }
    }
}