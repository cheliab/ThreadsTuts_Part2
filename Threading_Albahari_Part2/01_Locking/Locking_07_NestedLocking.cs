using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Вложенная блокировка
    /// </summary>
    public class Locking_07_NestedLocking
    {
        private static object _locker = new object();
        
        public static void NestedLocking_lock()
        {
            // Поток может многократно блокировать один и тот же объект
            // вложенным ( реентерабельным )
            lock (_locker)
                lock (_locker)
                    lock (_locker)
                    {
                        
                    }
        }

        public static void NestedLocking_Monitor()
        {
            Monitor.Enter(_locker);
                Monitor.Enter(_locker);
                    Monitor.Enter(_locker);
                    
                    Monitor.Exit(_locker);
                Monitor.Exit(_locker);
            Monitor.Exit(_locker);
        }
        
        // В этих сценариях объект разблокируется только тогда,
        // когда завершился самый внешний lock оператор
        // или было выполнено соответствующее количество
        // Monitor.Exit операторов.

        // Вложенная блокировка полезна,
        // когда один метод вызывает другой внутри блокировки
        public static void Start()
        {
            lock (_locker)
            {
                AnotherMethod();
                // В этот момент все еще есть блокировка
            }
        }

        private static void AnotherMethod()
        {
            lock (_locker)
            {
                Console.WriteLine("");
            }
        }
    }
}