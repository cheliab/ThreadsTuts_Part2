using System;
using System.Threading;

namespace Threading_Albahari_Part2._03_EventWaitHandles
{
    /// <summary>
    /// AutoResetEvent
    ///
    /// AutoResetEvent похож на турникет: вставив билет, можно пройти ровно одному человеку.
    ///  «Авто» в названии класса означает, что открытый турникет автоматически закрывается
    /// или «сбрасывается» после того, как кто-то переступит порог.
    ///
    /// Поток ожидает или блокирует турникет, вызывая WaitOne
    /// (ждите на этом «одном» турникете, пока он не откроется),
    /// и билет вставляется путем вызова Setметода.
    ///
    /// При вызове нескольких потоков WaitOne за турникетом выстраивается очередь.
    /// (Как и в случае с блокировками, справедливость очереди иногда может быть нарушена
    /// из-за нюансов в операционной системе).
    ///
    /// Билет может быть получен из любого потока;
    /// другими словами, любой (разблокированный) поток с доступом к AutoResetEvent
    /// объекту может вызвать Set его, чтобы освободить один заблокированный поток.
    ///
    /// </summary>
    public class EventWaitHandles_01_AutoResetEvent
    {
        private static void Create_AutoResetEvent_Example()
        {
            // Экземпляр AutoResetEvent можно создать двумя способами.
            //
            // Первый - через конструктор:
            var auto1 = new AutoResetEvent(false);
            
            // (Передача true в конструктор эквивалентна его немедленному вызову Set().)
            
            // И второй способ используя EventWaitHandle:
            var auto2 = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        private static EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public static void Start()
        {
            new Thread(WaiterWorker).Start(); // Запускаем второй поток
            
            Thread.Sleep(5000); // ждем 5 секунд

            _waitHandle.Set(); // раблокируем ожидающий поток
        }

        private static void WaiterWorker()
        {
            Console.WriteLine("Thread 2 - Start");
            
            _waitHandle.WaitOne(); // второй поток дойдет до этого места и будет ждать Set();
            
            Console.WriteLine("Thread 2 - Finish"); // (т.е. этот код выполнится только через 5 секунд)
        }
        
        // - Многократный вызов Set()
        
        // Если Set вызывается, когда ни один поток не ожидает,
        // дескриптор остается открытым до тех пор, пока не вызовет какой-либо поток WaitOne.
        //
        // Такое поведение помогает избежать гонки между заголовком потока для турникета и потоком,
        // вставляющим билет («Упс, билет вставлен на микросекунду слишком рано, неудача, теперь вам придется ждать бесконечно!»).
        //
        // Однако несколько вызовов Set подрят не имеют накопительный эффект.
        // Следующие вызовы WaitOne будут пропускать только 1 поток за раз.
        // Все "накопленные" вызовы Set не будут учитываться.

        public static void MultipleSetCall()
        {
            _waitHandle.Set();
            _waitHandle.Set();
            _waitHandle.Set();
            
            new Thread(WaiterWorker).Start(); // Отработает до конца только первый поток
            new Thread(WaiterWorker).Start(); // (остальные выполнятся до WaitOne())
            new Thread(WaiterWorker).Start();
        }
        
        // - Вызов Reset()
        
        // Вызов AutoResetEvent.Reset() закрывает "турникет" (если он был открыт).
        // Т.е. если до этого был вызван Set(), то это это уже не будет учитываться.

        public static void CallReset()
        {
            _waitHandle.Set();
            
            _waitHandle.Reset();
            
            new Thread(WaiterWorker).Start(); // поток не будет выполнен до конца (Reset() сбросил Set())
        }
    }
}