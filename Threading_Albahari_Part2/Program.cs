using System;

namespace Threading_Albahari_Part2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Locking_01.Start_ThreadUnsafe();
            Locking_01.Start_ThreadSafe();
            
            Console.ReadLine();
        }
    }
}