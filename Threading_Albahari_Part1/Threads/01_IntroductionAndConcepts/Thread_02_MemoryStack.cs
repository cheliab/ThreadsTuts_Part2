using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Каждый поток имеет свой стек
    ///
    /// В каждом потоке своя переменная 'cycles'
    /// </summary>
    public class Thread_02_MemoryStack
    {
        public static void Start()
        {
            new Thread(Go).Start(); // В этом потоке своя переменная 'cycles'
            
            Go(); // в главном потоке будет своя переменная 'cycle'
        }
        
        private static void Go()
        {
            for (int cycles = 0; cycles < 5; cycles++)
                Console.Write($"{cycles}");
        }
    }
}