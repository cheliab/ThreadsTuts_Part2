using System;
using System.Threading;

namespace DeadlockResolve_AccountExample
{
    /// <summary>
    /// Пример решения проблемы взаимоблокировки
    ///
    /// Основная логика для этого в методе AccountManager.Transfer()
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Startd");

            Account accountA = new Account(101, 5000);
            Account accountB = new Account(202, 7000);

            AccountManager accountManager_FromAToB = new AccountManager(accountA, accountB, 1000);
            Thread thread_AtoB = new Thread(accountManager_FromAToB.Transfer);
            thread_AtoB.Name = "Thread A to B";

            AccountManager accountManager_FromBToA = new AccountManager(accountB, accountA, 2000);
            Thread thread_BtoA = new Thread(accountManager_FromBToA.Transfer);
            thread_BtoA.Name = "Thread B to A";
            
            thread_AtoB.Start();
            thread_BtoA.Start();

            thread_AtoB.Join();
            thread_BtoA.Join();
            
            Console.WriteLine("Main Completed");
        }
    }
}