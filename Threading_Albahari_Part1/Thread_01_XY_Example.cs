using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Простой пример нескольких потоков
    ///
    /// http://www.albahari.com/threading/
    /// </summary>
    public class Thread_01_XY_Example
    {
        public static void Start()
        {
            // В новго потоке пишем "Y" в консоль
            Thread thread = new Thread(WriteY);
            thread.Start();
            
            // В основном потоке пишем "X" в консоль
            WriteX();
        }

        private static void WriteX()
        {
            for (int i = 0; i < 300; i++)
                Console.Write("x");
        }

        private static void WriteY()
        {
            for (var i = 0; i < 300; i++)
                Console.Write("y");
        }
    }
}