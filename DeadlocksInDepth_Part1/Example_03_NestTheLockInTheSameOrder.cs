using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlocksInDepth_Part1
{
    public class Account
    {
        public uint Id { get; set; }
    }
    
    /// <summary>
    /// Пример лечения дедлока с помощью порядка вложенных блокировок
    /// </summary>
    public class Example_03_NestTheLockInTheSameOrder
    {
        public static void DoDoubleTransfer_WithDeadlock()
        {
            Console.WriteLine("Start transfer...");
            
            var acc1 = new Account { Id = 1 };
            var acc2 = new Account { Id = 2 };

            var task1 = Transfer_WithDeadlock(acc1, acc2, 500);
            var task2 = Transfer_WithDeadlock(acc2, acc1, 600);

            Task.WaitAll(task1, task2);

            Console.WriteLine("Transfer finished...");
        }
        
        /// <summary>
        /// Пример метода с дедлоком
        /// </summary>
        private static Task Transfer_WithDeadlock(Account acc1, Account acc2, int sum)
        {
            var task = Task.Run(() =>
            {
                lock (acc1)
                {
                    Thread.Sleep(1000);
                    lock (acc2)
                    {
                        Console.WriteLine($"Finished transfering sum {sum}");
                    }
                }
            });

            return task;
        }

        
        public static void DoDoubleTransfer_IdSort()
        {
            Console.WriteLine("Start transfer...");

            var acc1 = new Account { Id = 1 };
            var acc2 = new Account { Id = 2 };

            var task1 = Transfer_IdSort(acc1, acc2, 100);
            var task2 = Transfer_IdSort(acc2, acc1, 200);

            Task.WaitAll(task1, task2);
            
            Console.WriteLine("Transfer finished...");
        }

        /// <summary>
        /// Сортировка блокируемых объектов для избавления от дедлока
        /// </summary>
        private static Task Transfer_IdSort(Account acc1, Account acc2, int sum)
        {
            var lock1 = acc1.Id < acc2.Id ? acc1 : acc2; // smalled Id account
            var lock2 = acc1.Id > acc2.Id ? acc1 : acc2; // biggest Id account
            
            var task = Task.Run(() =>
            {
                lock (lock1)
                {
                    Thread.Sleep(1000);
                    lock (lock2)
                    {
                        Console.WriteLine($"Finished transfering sum {sum}");
                    }
                }
            });

            return task;
        }

        /// <summary>
        /// Пример решения дедлока через таймауты
        ///
        /// Если не получилось заблокировать то просто уходим на новый круг
        /// </summary>
        private static Task Transfer_MonitorTimeout(Account acc1, Account acc2, int sum)
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        bool entered = Monitor.TryEnter(acc1, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;

                        entered = Monitor.TryEnter(acc2, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;

                        // Делаем расчеты
                        Console.WriteLine($"Finished transferring sum {sum}");

                        break;
                    }
                    finally
                    {
                        if (Monitor.IsEntered(acc1)) Monitor.Exit(acc1);
                        if (Monitor.IsEntered(acc2)) Monitor.Exit(acc2);
                        
                        // Делаем паузу перед началом нового цикла
                        Thread.Sleep(200);
                    }
                }    
            });
            
            return task;
        }
    }
}