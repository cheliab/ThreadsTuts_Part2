using System;

namespace DeadlocksInDepth_Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example_01_Singleton.Start();
            // Example_02_Deadlock_NestedLocks.Start();
            // Example_03_NestTheLockInTheSameOrder.DoDoubleTransfer_WithDeadlock();
            Example_03_NestTheLockInTheSameOrder.DoDoubleTransfer_IdSort();
            
            Console.ReadLine();
        }
    }
}