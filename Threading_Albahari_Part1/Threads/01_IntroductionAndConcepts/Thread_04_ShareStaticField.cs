using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Пример подобный предыдущему, только со статической переменной
    /// </summary>
    public class Thread_04_ShareStaticField
    {
        /// <summary>
        /// Статическое поле используемое в нескольких потоках
        /// </summary>
        private static bool _done;

        public static void Start()
        {
            new Thread(Go).Start();
            
            Go();
        }

        static void Go()
        {
            if (!_done)
            {
                _done = true;

                Console.WriteLine("Done");
            }
        }
    }
}