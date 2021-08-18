using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    public class Locking_02_Monitor_EnterExit
    {
        private static object _locker = new object();

        private static int _a = 1, _b = 1;
        
        public static void Start()
        {
            new Thread(Divide).Start();
            
            Thread.Sleep(1500);
            
            Divide();
        }

        private static void Divide()
        {
            Monitor.Enter(_locker);
            try
            {
                Thread.Sleep(1000);
                if (_b != 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(_a / _b);
                }
                else
                {
                    Console.WriteLine("Divide by zero");
                }

                Thread.Sleep(1000);
                _b = 0;
            }
            finally
            {
                Monitor.Exit(_locker);
            }
        }
    }
}