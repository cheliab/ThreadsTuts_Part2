using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Пример ошибки с использование лябда выражений
    /// </summary>
    public class Thread_10_LambdaExp_CapturedVar
    {

        public static void Start_forLoopExample()
        {
            // Проблема в том,
            // что i переменная ссылается на одну и ту же ячейку памяти
            // на протяжении всего цикла.
            // Следовательно, каждый поток вызывает Console.Write переменную,
            // значение которой может меняться во время работы!
            
            // Вариант вывода: 32225671089
            // Проблема в повторах "222"
            for (int i = 0; i < 10; i++)
                new Thread(() => Console.Write(i)).Start();
        }

        public static void Start_TempVarSolution()
        {
            // При использование временной переменной все значения будут разные
            // Так как ссылаются на разные ячейки памяти
            // Вариант вывода: 1203456789
            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                new Thread(() => Console.Write(temp)).Start();
            }
        }

        public static void Start_StringExample()
        {
            string text = "t1";
            Thread thread_1 = new Thread(() => Console.WriteLine(text));

            text = "t2";
            Thread thread_2 = new Thread(() => Console.WriteLine(text));
            
            thread_1.Start();
            thread_2.Start();
        }
    }
}