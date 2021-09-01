using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Пример что переменная может не успеть записаться при использовании в нескольких потоках
    /// если нет никакой блокировки
    /// </summary>
    public class Thread_05_BrokeShareData
    {
        /// <summary>
        /// Статическое поле используемое в нескольких потоках
        /// </summary>
        private static bool _done;

        public static void Start()
        {
            new Thread(Go).Start(); // "Done"
            
            Go(); // тоже может успеть вывести - "Done"
        }

        static void Go()
        {
            if (!_done) // Может успеть выполниться 2 раза
            {
                Console.WriteLine("Done"); // Может вывестись в косоль 2 раза
                
                _done = true; // Может не успеть измениться для 2 потока
            }
        }
    }
}