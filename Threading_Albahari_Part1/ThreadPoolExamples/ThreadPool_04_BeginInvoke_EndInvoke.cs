using System;
using System.Reflection.Metadata;

namespace Threading_Albahari_Part1.ThreadPoolExamples
{
    public class ThreadPool_04_BeginInvoke_EndInvoke
    {
        public static void Start()
        {
            Func<string, int> method = Work;
            IAsyncResult cookie = method.BeginInvoke("test", null, null);

            // тут может паралельно выполняться другая работа
            
            // тут происходит получение результата метода
            // (если метод еще не завершится текущий поток блокируется)
            int result = method.EndInvoke(cookie); 
        }

        private static int Work(string s)
        {
            return s.Length;
        }
    }
}