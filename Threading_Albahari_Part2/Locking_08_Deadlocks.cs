using System.Threading;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Deadlock (Взаимоблокировка потоков)
    /// </summary>
    public class Locking_08_Deadlocks
    {
        private static object _locker1 = new object();
        private static object _locker2 = new object();

        public static void Start()
        {
            new Thread(() =>
            {
                lock (_locker1)
                {
                    Thread.Sleep(1000);
                    lock (_locker2); // Deadlock
                }
            }).Start();

            lock (_locker2)
            {
                Thread.Sleep(1000);
                lock (_locker1); // Deadlock
            }
        }
    }
}