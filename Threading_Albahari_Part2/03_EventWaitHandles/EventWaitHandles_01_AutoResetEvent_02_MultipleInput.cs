using System;
using System.Threading;

namespace Threading_Albahari_Part2._03_EventWaitHandles
{
    public class EventWaitHandles_01_AutoResetEvent_02_MultipleInput
    {
        private static AutoResetEvent _waitHandler = new AutoResetEvent(false);
        
        public static void Start()
        {
            new Thread(Worker).Start(); // Стартуем второй поток
            
            // ----------------------------------------

            Console.ReadLine(); // Первый ввод

            _waitHandler.Set(); // Разблокировли первый шаг

            Console.ReadLine(); // Второй ввод

            _waitHandler.Set(); // Разблокировли второй шаг
        }

        private static void Worker()
        {
            Console.WriteLine("1");

            _waitHandler.WaitOne(); // Ожидаем первый ввод
            
            Console.WriteLine("2");

            _waitHandler.WaitOne(); // Ожидаем второй ввод
            
            Console.WriteLine("3");
        }
    }
}