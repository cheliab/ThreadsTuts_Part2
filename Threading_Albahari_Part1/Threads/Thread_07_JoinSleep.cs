using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    public class Thread_07_JoinSleep
    {
        public static void Start()
        {
            Thread thread = new Thread(Go);
            thread.Start();
            thread.Join(); 
            // Главный поток не продолжит выполняться
            // пока не выполнится новый поток
            
            Console.WriteLine("Thread thread has ended!");
        }

        private static void Go()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write("y");
                Thread.Sleep(200);
            }    
        }
    }
}