using System;
using System.Threading;

namespace Threading_Albahari_Part2
{
    // A Mutex похож на C # lock,
    // но может работать с несколькими процессами.
    // Другими словами, он Mutex может быть как в масштабе компьютера, так и в масштабе приложения .
    public class Locking_09_Mutex
    {
        public static void Start()
        {
            using (var mutex = new Mutex(false, "berezki.p app - Locking_09_Mutex)"))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
                {
                    Console.WriteLine("Another app instance is running. Bye!");
                    return;
                }

                RunProgram();
            }
        }

        private static void RunProgram()
        {
            Console.WriteLine("Running. Press Enter to exit.");
        }
    }
}