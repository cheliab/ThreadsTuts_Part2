using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    public class Locking_01
    {
        public static void Start()
        {
            for (int i = 0; i < 100; i++) {
                new Thread(ThreadUnsafe.Go).Start();        
            }    
        }
    }

    /// <summary>
    /// Пример не потоко безопасного метода
    /// </summary>
    class ThreadUnsafe
    {
        private static int _val1 = 1, _val2 = 1;

        public static void Go()
        {
            if (_val2 != 0)
                Console.WriteLine(_val1 / _val2);

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

        public static void Go()
        {
            lock (_locker) // теперь только 1 поток может исполнять данный кусок кода
            {
                if (_val2 != 0)
                    Console.WriteLine(_val1 / _val2);

                _val2 = 0;
            }
        }
    }
}