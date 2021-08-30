using System;
using System.Threading;

namespace Threading_Albahari_Part2._03_EventWaitHandles
{
    // Доп. пример с ожиданием ввода от пользователя
    //
    // https://www.youtube.com/watch?v=xaaRBh07N34
    
    public class EventWaitHandles_01_AutoResetEvent_01_WaitInput
    {
        private static AutoResetEvent _waitHandle = new AutoResetEvent(false);
        
        public static void Start()
        {
            // Запускаем поток который будет ждать пока пользотель что-то введет
            new Thread(Worker).Start();

            Console.ReadLine(); // Ввод 
            _waitHandle.Set(); // После того как ввели что-то, поток воркер разблокируется и продолжит выполнение
        }

        private static void Worker()
        {
            Console.WriteLine("Starting...");
            
            _waitHandle.WaitOne(); // блокируем поток, пока не будет получен сигнал что можно продолжить
            
            Console.WriteLine("Finishing..."); // выведется только после того как пользовтель введет данные
        }
    }
}