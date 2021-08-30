using System;
using Threading_Albahari_Part2._03_EventWaitHandles;

namespace Threading_Albahari_Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Locking_01.Start_ThreadUnsafe();
            // Locking_01.Start_ThreadSafe();
            // Locking_02_Monitor_EnterExit.Start();
            
            // Locking_09_Mutex.Start();
            // Locking_10_Semaphore.Start();

            // ThreadSafety_01_NETFrameworkTypes.Start();
            // ThreadSafety_02_ApplicationServers.Start();
            // ThreadSafety_03_ImmutableObjects.Start();
            
            // EventWaitHandles_01_AutoResetEvent.Start();
            // EventWaitHandles_01_AutoResetEvent.MultipleSetCall();
            EventWaitHandles_01_AutoResetEvent.CallReset();
            
            Console.ReadLine();
        }
    }
}