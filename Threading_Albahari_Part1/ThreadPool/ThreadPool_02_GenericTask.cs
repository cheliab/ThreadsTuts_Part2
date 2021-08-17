using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_Albahari_Part1.ThreadPool
{
    /// <summary>
    /// Создание универсального класса Task 
    /// </summary>
    public class ThreadPool_02_GenericTask
    {
        public static void Start()
        {
            Task<string> task = Task.Factory.StartNew<string>(() => DownloadString("http://www.linqpad.net"));
            
            // параллельно может выполняться другая работа
            SomeOtherMethod();

            // Когда нам нужно получить значение из задачи, мы запрашиваем его свойство "Result" 
            // Если задача все еще выполняется, текущий поток будет заблокирован до завершения задачи. 
            string result = task.Result;
            Console.WriteLine(result.Length);
        }

        private static string DownloadString(string uri)
        {
            Console.WriteLine("Start download");
            
            using var webClient = new WebClient();
            return webClient.DownloadString(uri);
        }

        private static void SomeOtherMethod()
        {
            // Thread.Sleep(500);
            Console.WriteLine("bla bla");
        }
    }
}