using System;

namespace Threading_Albahari_Part1.ThreadPoolExamples
{
    /// <summary>
    /// Использование callback метода в BeginInvoke для обработки результата
    /// </summary>
    public class ThreadPool_05_BeginInvokeCallback
    {
        public static void Start()
        {
            Func<string, int> method = Work;
            method.BeginInvoke("тест", DoneCallback, method); // Не выполняется на .net 5 на .net framework .4.7 норм
        }

        private static int Work(string s)
        {
            return s.Length;
        }

        private static void DoneCallback(IAsyncResult cookie)
        {
            var target = (Func<string, int>)cookie.AsyncState;
            int result = target.EndInvoke(cookie);
            
            Console.WriteLine($"String length is {result}");
        }
    }
}