using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Обработка исключений в потоках
    ///
    /// Любые блоки try/catch/finally в области видимости при создании потока не имеют отношения к потоку, когда он начинает выполнение.
    /// </summary>
    public class Thread_14_ExceptionHandling
    {
        public static void Start()
        {
            // Go(); // NRE вызывается

            ThreadStart_1();
            // ThreadStart_2();
        }
        
        /// <summary>
        /// Пример когда try catch не отработает
        /// </summary>
        private static void ThreadStart_1()
        {
            try
            {
                new Thread(Go).Start(); // Вызывается метод с NRE
            }
            catch (Exception ex)
            {
                // Не попадем сюда не смотря на NRE
                Console.WriteLine("Exception!");
            }
        }
        
        private static void Go()
        {
            throw null; // кидаем NRE
        }

        /// <summary>
        /// Пример когда отработает
        /// </summary>
        private static void ThreadStart_2()
        {
            new Thread(Go_WithTryCatch).Start();
        }

        /// <summary>
        /// Чтобы отловить исключение можно все в методе обернуть в try catch
        /// </summary>
        /// <exception cref="Exception"></exception>
        private static void Go_WithTryCatch()
        {
            try
            {
                throw null; // Исключение появится и будет обработано ниже
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception!");
            }
        }
    }
}