using System.Collections.Generic;
using System.Threading;

// Пример из статьи
// https://www.codeproject.com/Articles/28785/Thread-synchronization-Wait-and-Pulse-demystified

namespace ThreadSynq_Monitor_BlockingQueue
{
    public class BlockingQueue<T>
    {
        /// <summary>
        /// Размер очереди
        /// </summary>
        private readonly int _Size = 0;
        
        /// <summary>
        /// Очередь
        /// </summary>
        private readonly Queue<T> _Queue = new Queue<T>();
        
        /// <summary>
        /// Объект блокировки
        /// </summary>
        private readonly object _Key = new object();

        /// <summary>
        /// Остановка очереди
        /// </summary>
        private bool _Quit = false;

        public BlockingQueue(int size)
        {
            _Size = size;
        }

        /// <summary>
        /// Остановка очереди
        /// </summary>
        public void Quit()
        {
            lock (_Key)
            {
                _Quit = true;
                
                // Оповестить все заблокированные потоки
                Monitor.PulseAll(_Key);
                // Все потоки ожидающие объект блокировки перейдут в состоние готовности
                // Они будут "разбужены" или по другому переместятся в список готовности
            }
        }

        /// <summary>
        /// Добавить элемент в очередь
        /// </summary>
        /// <param name="t">Добавляемый элемент</param>
        /// <returns>Результат добавления</returns>
        public bool Enqueue(T t)
        {
            lock (_Key)
            {
                // Блокируем поток, если очередь заполнена или остановлена
                while (!_Quit && _Queue.Count >= _Size)
                    Monitor.Wait(_Key);

                // Не добавляем в очередь, если она остановлена
                if (_Quit)
                    return false;
                
                _Queue.Enqueue(t);
                
                // Оповестить все заблокированные потоки
                Monitor.PulseAll(_Key);
                // Все потоки ожидающие объект блокировки перейдут в состоние готовности
                // Они будут "разбужены" или по другому переместятся в список готовности
            }

            return true;
        }

        /// <summary>
        /// Получить элемент из очереди
        /// </summary>
        /// <param name="t">Полученный из очереди элемент</param>
        /// <returns>Результат получения значения</returns>
        public bool Dequeue(out T t)
        {
            t = default(T);

            lock (_Key)
            {
                // Блокируем поток, если очередь пустая или остановлена 
                while (!_Quit && _Queue.Count == 0)
                    Monitor.Wait(_Key);

                // Не получаем значение, если очередь пустая
                if (_Queue.Count == 0)
                    return false;

                t = _Queue.Dequeue();
                
                // Оповестить все заблокированные потоки
                Monitor.PulseAll(_Key);
                // Все потоки ожидающие объект блокировки перейдут в состоние готовности
                // Они будут "разбужены" или по другому переместятся в список готовности
            }

            return true;
        }
    }
}