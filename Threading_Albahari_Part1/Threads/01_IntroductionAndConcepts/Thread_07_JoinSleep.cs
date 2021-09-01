using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Присоединяйся и спи (по версии гугл переводчика)
    ///
    /// Join and Sleep
    ///
    /// Один поток может дождаться завершения другого потока, вызвав Join метод у экземпляра этого потока
    /// </summary>
    public class Thread_07_JoinSleep
    {
        public static void Start()
        {
            Thread thread = new Thread(Go);
            thread.Start();
            thread.Join(); 
            // Главный поток не продолжит выполняться
            // пока не выполнится новый поток
            
            Console.WriteLine("Thread thread has ended!"); // Выведется в консоль в самом конце
        }

        private static void Go()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("y");
                Thread.Sleep(200); // Ожидание 200 миллисекунд
            }    
        }

        /// <summary>
        /// Примеры использования Thread.Sleep()
        /// </summary>
        private static void ThreadSleepExamples()
        {
            Thread.Sleep(TimeSpan.FromHours(1)); // поток блокируется на 1 час
            Thread.Sleep(500); // поток блокируется на 500 миллисекунд
            
            Thread.Sleep(0); // Высвобождается текущий квант(такт) времени
            // Если есть поток с большим приоритетом то планировщик переключится на него
        }

        /// <summary>
        /// Пример использования Thead.Yield() метода
        /// </summary>
        private static void ThreadYieldExample()
        {
            Thread.Yield(); // аналогичен Sleep(0), но уступается потокам на том же процессоре(ядре?)
        }
    }
}