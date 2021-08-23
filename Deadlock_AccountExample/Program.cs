using System;
using System.Threading;

namespace Deadlock_AccountExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Started");

            Account accountA = new Account(101, 5000);
            Account accountB = new Account(202, 3000);

            AccountManager accountManagerA = new AccountManager(accountA, accountB, 1000);
            Thread T1 = new Thread(accountManagerA.Transfer);
            T1.Name = "T1";

            AccountManager accountManagerB = new AccountManager(accountB, accountA, 2000);
            Thread T2 = new Thread(accountManagerB.Transfer);
            T2.Name = "T2";
            
            T1.Start();
            T2.Start();
            
            // После старта призойдет взаимоблокировка
            // так как каждый из потоков будет ждать пока разблокируется чужой аккаунт

            T1.Join();
            T2.Join();
            
            Console.WriteLine("Main Completed");
        }
    }
}