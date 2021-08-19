namespace Threading_Albahari_Part2
{
    // http://www.albahari.com/threading/part2.aspx#_When_to_Lock
    //
    // When to Lock
    //
    // Когда стоит блокировать
    //
    // Как правило, стоит блокировать доступ к любому общему полю, доступному для записи.

    /// <summary>
    /// Не потокобезопасный вариант
    /// </summary>
    class ThreadUnsafe_05
    {
        private static int _x;

        static void Increment()
        {
            _x++;
        }

        static void Assing()
        {
            _x = 123;
        }
    }

    /// <summary>
    /// Потокобезопасный вариант
    /// </summary>
    class ThreadSafe_05
    {
        private static readonly object _locker = new object();
        private static int _x;

        static void Increment()
        {
            lock (_locker) _x++;
        }
        
        static void Assing()
        {
            lock (_locker) _x = 123;
        }
    }
}