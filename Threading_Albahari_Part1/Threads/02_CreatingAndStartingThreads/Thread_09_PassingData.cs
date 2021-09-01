using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Варианты для передачи данных в поток
    /// </summary>
    public class Thread_09_PassingData
    {
        public static void Start()
        {
            // Запуск метода из лямбда выражения
            Thread thread = new Thread(() => Print("Hello from thread!")); // Вариант удобен тем что можно использовать любое количество параметров
            thread.Start();
            
            // Выполнение нескольких инстуркций в лямбда выражении
            new Thread(() =>
            {
                Console.WriteLine("i'm running on another thread!");
                Console.WriteLine("This is so easy");
            }).Start();
            
            // Использование анонимного метода в отдельном потоке
            new Thread(delegate()
            {
                Console.WriteLine("Delegate in Thread");
            }).Start();
            
            // Передача параметров в виде объекта
            // Используется делегат ParameterizedThreadStart:
            // public delegate void ParameterizedThreadStart(object obj);
            
            new Thread(PrintObject).Start("Object message"); // объект передается через метод Start(object parameter)
        }
        
        private static void Print(string message)
        {
            Console.WriteLine(message);
        }

        private static void PrintObject(object messageObj)
        {
            string message = (string)messageObj;
            
            Console.WriteLine(message);
        }
    }
}