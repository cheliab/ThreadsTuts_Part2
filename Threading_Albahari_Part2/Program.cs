﻿using System;

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
            ThreadSafety_02_ApplicationServers.Start();
            
            Console.ReadLine();
        }
    }
}