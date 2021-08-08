using System;
using System.Threading;


namespace ParameterizedThread_ClassParam
{
    /// <summary>
    /// Примеры передачи параметров в поток
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // StartThread_SimpleClass();
            // StartThread_SpecialTypeSaveClass();
            // StartThread_UseTuple();
            StartThread_UseLambda();
            
            Console.ReadLine();
        }

        /// <summary>
        /// Использование лябда выражения для передачи параметров
        /// </summary>
        private static void StartThread_UseLambda()
        {
            int x = 5;
            int y = 4;

            Thread newThread = new Thread(() => Count(x, y));
            newThread.Start();
        }

        private static void Count(int x, int y)
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * x * y);
            }
        }

        /// <summary>
        /// Использование кортежа для передачи параметров в поток
        /// </summary>
        private static void StartThread_UseTuple()
        {
            (int x, int y) tuple = (5, 4);

            Thread newThread = new Thread(new ParameterizedThreadStart(Count_TupleVersion));
            newThread.Start(tuple);
        }

        private static void Count_TupleVersion(object param)
        {
            (int x, int y) tuple = (ValueTuple<int, int>)param;

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * tuple.x * tuple.y);
                
                Thread.Sleep(400);
            }
        }

        /// <summary>
        /// Пример использования специального класса для обработки в отдельном потоке
        ///
        /// Такой класс позволяет безопасно по типам передать параметры в метод потока
        /// </summary>
        private static void StartThread_SpecialTypeSaveClass()
        {
            var counter = new Counter_TypeSave(5, 4);

            Thread newThread = new Thread(new ThreadStart(counter.Count));
            newThread.Start();
        } 

        /// <summary>
        /// Пример использования класса как параметра
        ///
        /// Позволяет передать несколько параметров в поток
        /// Не типо безопасно, так как Start принимает классы любого типа
        /// </summary>
        private static void StartThread_SimpleClass()
        {
            Counter counter = new Counter();
            counter.x = 4;
            counter.y = 5;

            Thread newThread = new Thread(new ParameterizedThreadStart(Count));
            // Thread newThread = new Thread(Count);
            newThread.Start(counter);
        }

        private static void Count(object obj)
        {
            Counter c = (Counter)obj;
            
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * c.x * c.y);
            }
        }
    }
}