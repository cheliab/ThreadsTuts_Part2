using System;
using System.Threading;

namespace ParameterizedThread_ClassParam
{
    /// <summary>
    /// Пример специального класса для использования в потоке
    ///
    /// За счет такого подхода получается безопасная типизация
    /// при использовании класса в потоке
    /// </summary>
    public class Counter_TypeSave
    {
        private int _x;
        private int _y;

        public Counter_TypeSave(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * _x * _y);

                Thread.Sleep(400);
            }
        }
    }
}