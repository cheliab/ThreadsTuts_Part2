using System.Threading;

namespace Deadlock_AccountExample
{
    public class AccountManager
    {
        private Account _fromAccount;
        private Account _toAccount;
        private double _amountToTransfer;

        public AccountManager(Account fromAccount, Account toAccount, double amountToTransfer)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amountToTransfer = amountToTransfer;
        }

        /// <summary>
        /// Перевод денег с одного счета на другой
        ///
        /// тут возможен deadlock
        /// </summary>
        public void Transfer()
        {
            lock (_fromAccount)
            {
                Thread.Sleep(1000);
                lock (_toAccount) // тут deadlock
                {
                    // операции внутри уже не выполняться
                    
                    _fromAccount.Withdraw(_amountToTransfer);
                    _toAccount.Deposit(_amountToTransfer);
                }
            }
        }
    }
}