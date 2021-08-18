using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    public class Locking_01
    {
        public static void Start_ThreadUnsafe()
        {
            new Thread(ThreadUnsafe.Divide).Start();
            
            Thread.Sleep(1750); // Сдвиг, чтобы попасть между проверкой условия и делением
            
            ThreadUnsafe.Divide();
        }

        public static void Start_ThreadSafe()
        {
            new Thread(ThreadSafe.Divide).Start();
            
            Thread.Sleep(1750);
            
            ThreadSafe.Divide();
        }
    }

    /// <summary>
    /// Пример не потоко безопасного метода
    /// </summary>
    class ThreadUnsafe
    {
        private static int _val1 = 1, _val2 = 1;

        public static void Divide()
        {
            Thread.Sleep(1000); // задержка между операциями, чтобы проще словить деление на ноль
            if (_val2 != 0)
            {
                Thread.Sleep(1000); // В этом месте второй поток может изменить значение _val2 на 0
                Console.WriteLine(_val1 / _val2);
            }

            Thread.Sleep(1000);
            _val2 = 0;
        }
    }

    /// <summary>
    /// Пример потоко безопасного метода
    /// </summary>
    class ThreadSafe
    {
        /// <summary>
        /// Объект блокировки
        /// </summary>
        private static readonly object _locker = new object();
        
        /// <summary>
        /// Защищаемые поля
        /// </summary>
        private static int _val1 = 1, _val2 = 1;

        public static void Divide()
        {
            lock (_locker) // теперь только 1 поток может исполнять данный кусок кода
            {
                Thread.Sleep(1000);
                if (_val2 != 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(_val1 / _val2);
                }
                    
                Thread.Sleep(1000);
                _val2 = 0;
            }
        }
    }
}