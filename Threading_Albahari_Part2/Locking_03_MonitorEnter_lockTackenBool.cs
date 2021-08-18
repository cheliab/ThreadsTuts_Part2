using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    public class Locking_03_MonitorEnter_lockTackenBool
    {
        private static object _locker = new object();
        private static int _a = 1, _b = 1;
        
        // Пример с проблемой - Возможна потеря объекта блокировки
        // http://www.albahari.com/threading/part2.aspx#_lockTaken_overloads
        private static void Problem()
        {
            // Рассмотрим (маловероятное) событие исключения,
            // генерируемого внутри реализации Monitor.Enter или между вызовом Monitor.Enter и try блоком
            // (возможно, из-за Abort того, что было вызвано в этом потоке - или из-за того, OutOfMemoryException что оно было выброшено).
            // В таком сценарии блокировка может быть взята, а может и нет.
            // Если блокировка будет снята, она не будет снята, потому что мы никогда не войдем в блок try/ finally. Это приведет к утечке замка.
            
            Monitor.Enter(_locker);
            // тут может что-то сломаться и могут быть проблемы. Можем никогда не войти в блок try/finally.
            try
            {

            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }

        // Пример без проблемы
        private static void NoProblem()
        {
            // правильный шаблон использования (так C# с 4.0 версии компилирует lock оператор)
            bool lockTaken = false;
            try
            {
                Monitor.Enter(_locker, ref lockTaken);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(_locker);
            }
        }
    }
}