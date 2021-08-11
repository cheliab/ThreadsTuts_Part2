using System;
using System.Threading;

namespace ThreadSync_lock
{
    /// <summary>
    /// Класс часов
    /// </summary>
    public class TickTock
    {
        private object locker = new object();
        
        public void Tick(bool running)
        {
            lock (locker)
            {
                if (!running) // остановить часы
                {
                    Monitor.Pulse(locker);
                    return;
                }
                
                Console.Write("Tick ");
                
                Monitor.Pulse(locker); // дать методу "Tock" запуститься
                Monitor.Wait(locker); // ждать пока метод "Tock" завершится
            }
        }

        public void Tock(bool running)
        {
            lock (locker)
            {
                if (!running) // остановить часы
                {
                    Monitor.Pulse(locker);
                    return;
                }
                
                Console.WriteLine("Tock");
                
                Monitor.Pulse(locker); // дать методу "Tick" запуститься
                Monitor.Wait(locker); // ждать пока метод "Tick" завершится
            }
        }
    }

    public class MyThread
    {
        public Thread _thread;
        private TickTock _tickTock;

        public MyThread(string name, TickTock tickTock)
        {
            _tickTock = tickTock; // Передаем часы в поле

            // Создаем новый поток
            _thread = new Thread(Run); // Передаем метод
            _thread.Name = name; // Даем название новому потоку
            _thread.Start(); // Запускаем поток
        }

        /// <summary>
        /// Метод выполняемый потоком
        /// </summary>
        private void Run()
        {
            if (_thread.Name == "Tick")
            {
                for (int i = 0; i < 5; i++)
                    _tickTock.Tick(true);
                
                _tickTock.Tick(false);
            }
            else // _thread.Name == "Tock"
            {
                for (int i = 0; i < 5; i++)
                    _tickTock.Tock(true);
                
                _tickTock.Tock(false);
            }
        }
    }
    
    public class Monitor_TickTock
    {
        public static void Start()
        {
            TickTock tickTock = new TickTock();

            MyThread myThread_1 = new MyThread("Tick", tickTock);
            MyThread myThread_2 = new MyThread("Tock", tickTock);

            myThread_1._thread.Join(); // Основной поток "Main" ждет завершения потока 1
            myThread_2._thread.Join(); // Основной поток "Main" ждет завершения потока 2
            
            Console.WriteLine("Clock Stopped");
        }
    }
}