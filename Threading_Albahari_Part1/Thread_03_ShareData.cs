using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Можно использовать один экзепляр переменной в нескольких потоках
    /// </summary>
    public class Thread_03_ShareData
    {
        private bool _done;

        public static void Start()
        {
            // Создаем общий экзепляр
            Thread_03_ShareData commonInstance = new Thread_03_ShareData();
            
            new Thread(commonInstance.Go).Start(); // Запускаем метод общего экзепляра в отдельном потоке
            
            commonInstance.Go(); // так же запускаем этот метод в основном потоке
        }

        private void Go()
        {
            if (!_done) // Условие выполнится только 1 раз
            {
                // Меняем значение переменной
                _done = true;
                
                // Выведется 1 раз
                Console.WriteLine("Done");
            }
        }
    }
}