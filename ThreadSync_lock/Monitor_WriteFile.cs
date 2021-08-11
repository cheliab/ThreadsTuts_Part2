using System;
using System.IO;
using System.Threading;

namespace ThreadSync_lock
{
    /// <summary>
    /// Использование монитора для синхронизации записи в файл нескольких потоков
    /// </summary>
    public class Monitor_WriteFile
    {
        private static object locker = new object();

        public static void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(new ThreadStart(ThreadMain));
                thread.Name = $"Thead - {i}";
                thread.Start();
            }
        }

        private static void ThreadMain()
        {
            Thread.Sleep(1000);
            
            WriteToFile();
        }

        private static void WriteToFile()
        {
            Monitor.Enter(locker);
            
            string threadName = Thread.CurrentThread.Name;
            string fileName = "test.txt";

            var writeText = $"Thead - {threadName} - using file {fileName}";
            
            Console.WriteLine(writeText);

            try
            {
                using (var stream = new StreamWriter($@"D:\{fileName}", true))
                {
                    stream.WriteLine(writeText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Monitor.Exit(locker);
                
                Console.WriteLine($"Thead - {threadName} - releasing file {fileName}");
            }
        }
    }
}