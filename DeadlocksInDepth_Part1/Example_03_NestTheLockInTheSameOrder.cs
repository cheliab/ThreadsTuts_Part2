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
        public void DoDoubleTransfer_WithDeadlock()
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
        private Task Transfer_WithDeadlock(Account acc1, Account acc2, int sum)
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

        public void DoDoubleTransfer_NonDeadlock()
        {
            Console.WriteLine("Start transfer...");

            var acc1 = new Account { Id = 1 };
            var acc2 = new Account { Id = 2 };

            var task1 = Transfer_NonDeadlock(acc1, acc2, 100);
            var task2 = Transfer_NonDeadlock(acc2, acc1, 200);

            Task.WaitAll(task1, task2);
            
            Console.WriteLine("Transfer finished...");
        }

        private Task Transfer_NonDeadlock(Account acc1, Account acc2, int sum)
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
    }
}