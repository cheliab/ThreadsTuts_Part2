using System;
using System.Threading;

namespace DeadlocksInDepth_Part1
{
    /// <summary>
    /// Класс который должен быть 1 в приложении
    /// </summary>
    public class Singleton
    {
        
    }
    
    /// <summary>
    /// Пример потокобезопасного создания синглтона
    /// </summary>
    public class Example_01_Singleton
    {
        private static Singleton _instance = null;
        private static readonly object _locker = new object();

        public static void Start()
        {
            var thread1 = new Thread(GetSingletonInThread);
            var thread2 = new Thread(GetSingletonInThread);

            thread1.Start();
            
            Thread.Sleep(500); // Задержка чтобы попасть между проверкой и созданием экзепляра
            
            thread2.Start();
        }

        private static void GetSingletonInThread()
        {
            var singleton = GetInctance();
            Console.WriteLine(singleton.GetHashCode());
        }
        
        /// <summary>
        /// В таком варианте получим только 1 экзепляр объекта даже если метод будет запущен в нескольких потоках
        /// </summary>
        /// <returns></returns>
        public static Singleton GetInctance()
        {
            lock (_locker) // Если убрать блокировку то может получиться что два потока создадут разные экзепляры
            {
                Thread.Sleep(1000);
                if (_instance == null)
                {
                    Thread.Sleep(1000);
                    _instance = new Singleton();
                }
                    
                return _instance;
            }
        }
    }
}