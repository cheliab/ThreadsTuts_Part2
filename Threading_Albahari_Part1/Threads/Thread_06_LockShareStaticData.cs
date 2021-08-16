using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    public class Thread_06_LockShareStaticData
    {
        private static bool _done = false;

        private static readonly object locker = new object();

        public static void Start()
        {
            new Thread(Go).Start();
            
            Go();
        }

        private static void Go()
        {
            lock (locker)
            {
                if (!_done)
                {
                    Console.WriteLine("Done");
                    _done = true;
                }
            }
        }
    }
}