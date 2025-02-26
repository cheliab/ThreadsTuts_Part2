﻿using System;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Пример возможной ошибки при использование лябда выражений для запуска потока
    /// </summary>
    public class Thread_10_LambdaExp_CapturedVar
    {
        // Как мы видели, лямбда-выражение - это наиболее эффективный способ передачи данных в поток.
        // Однако вы должны быть осторожны, чтобы случайно не изменить захваченные переменные после запуска потока,
        // потому что эти переменные являются общими.

        public static void Start_forLoopExample()
        {
            // Проблема в том,
            // что i переменная ссылается на одну и ту же ячейку памяти
            // на протяжении всего цикла.
            // Следовательно, каждый поток вызывает Console.Write переменную,
            // значение которой может меняться во время работы.
            
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

        /// <summary>
        /// Мы можем более просто проиллюстрировать проблему в предыдущем коде на следующем примере
        /// </summary>
        public static void Start_StringExample()
        {
            string text = "t1";
            Thread thread_1 = new Thread(() => Console.WriteLine(text));

            text = "t2";
            Thread thread_2 = new Thread(() => Console.WriteLine(text));
            
            thread_1.Start(); // "t2"
            thread_2.Start(); // "t2"
            
            // Поскольку оба лямбда-выражения захватывают одну и ту же text переменную, "t2" печатается дважды.
        }
    }
}