using System;
using System.Collections.Generic;
using System.Threading;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Блокировка может использоваться для преобразования кода,
    /// небезопасного для потоков, в код, безопасный для потоков.
    ///
    /// Хорошим применением этого является .NET Framework: почти все его непримитивные типы не являются потокобезопасными
    /// (для чего-либо, кроме доступа только для чтения) при создании экземпляра,
    /// тем не менее их можно использовать в многопоточном коде, если весь доступ к любому заданному объекту защищен lock.
    ///
    /// Вот пример, где два потока одновременно добавляют элемент в одну и ту же List коллекцию, а затем перечисляют список:
    /// </summary>
    public class Locking_11_ThreadSafetyAndNETFrameworkTypes
    {
        private static List<string> _list = new List<string>();

        public static void Start()
        {
            var thread1 = new Thread(AddItem);
            var thread2 = new Thread(AddItem);

            thread1.Name = "1";
            thread2.Name = "2";
            
            thread1.Start();
            thread2.Start();

            // new Thread(AddItem_MethodLock).Start();
            // new Thread(AddItem_MethodLock).Start();
        }

        /// <summary>
        /// В этом случае мы фиксируем сам _list объект.
        /// 
        /// Если бы у нас было два взаимосвязанных списка,
        /// нам нужно было бы выбрать общий объект для блокировки (мы могли бы назначить один из списков, или лучше: использовать независимое поле).
        ///
        /// Перечисление коллекций .NET также небезопасно для потоков в том смысле,
        /// что возникает исключение, если список изменяется во время перечисления.
        ///
        /// Вместо блокировки на время перечисления в этом примере мы сначала копируем элементы в массив.
        /// Это позволяет избежать чрезмерного удержания блокировки, если то, что мы делаем во время перечисления, потенциально требует много времени.
        /// </summary>
        static void AddItem()
        {
            lock (_list)
                _list.Add($"Item {_list.Count};");

            string[] items;
            lock (_list)
                items = _list.ToArray();
            
            foreach(string s in items)
                Console.WriteLine($"Thread - {Thread.CurrentThread.Name}; {s}");
        }

        static void AddItem_MethodLock()
        {
            lock (_list)
            {
                _list.Add($"Item {_list.Count}");

                string[] items = _list.ToArray();
                
                foreach (var s in items)
                    Console.WriteLine($"Item {_list.Count}");
            }
        }
    }
}