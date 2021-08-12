using System;
using System.Threading;

namespace ThreadSync_lock
{
    /// <summary>
    /// Простой пример использования Wait Pulse методов класса Monitor
    /// </summary>
    public class Monitor_WaitPulse_SimpleExample
    {
        private static object locker = new object();

        private static int _x = 0;
        private static int _y = 0;

        public static void Start()
        {
            Thread thread = new Thread(Pulse);
            thread.Start();
            
            Wait();
        }

        private static void Wait()
        {
            lock (locker)
            {
                while (_x == 0)
                {
                    if (_y > 0)
                    {
                        Monitor.Pulse(locker); // Оповестить поток из очереди что можно заблокировать объект
                        break;
                    }
                    
                    Console.WriteLine("Ставим поток на ожидание - выполянем Wait");
                    Monitor.Wait(locker); // заблокировать текущий поток и отпустить блокировку (т.е. дальше код не пойдет пока другой поток не отпустить locker)
                    Console.WriteLine("Ожидание окончено продолжаем - после Wait");
                }

                Console.WriteLine("Цикл while закончился");
            }
        }

        private static void Pulse()
        {
            Thread.Sleep(5000); // ждем, чтобы этот поток не успел первым заблокировать locker

            lock (locker) // дойдя до этого места поток заблокируется, пока другой поток не отпустит locker методом Wait
            {
                Console.WriteLine("Посылаем сигнал потоку...");
                
                // Посылает сигнал заблокированному потоку, что можно встать в очередь ожидания объекта блокировки
                Monitor.Pulse(locker); 
                // Если закоментировать, то после снятия блокировки с locker не продолжится выполнение после Wait
                
                Console.WriteLine("Сигнал был отправлен...");
                
                // меняем условие выполнения цикла while (повторного выполнения цикла не должно произойти)
                _x = 1;
                // (если закоментировать первый поток уйдет в бесконечный цикл)
                // изменение зачения видимо показывает, что второй поток может поменять данные
                // и при продолжении выполнения первый поток будет вести себя по другому
                
                Thread.Sleep(5000); // ждем 5 сек
                
                Console.WriteLine("Sleep закончился...");
            } // Снятие блокировки с locker, ожидающий поток продолжится с места где выполнился Wait
        }
    }
}